using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Physics-based quadcopter controller for Unity 6.
/// Drives a Rigidbody using simulated motor thrust, an altitude-hold
/// throttle model, and a PD attitude controller — the same conceptual
/// model real flight controllers (and consumer drones in GPS/hover mode) use.
///
/// CONTROLS — Mode 2 (the standard FPV/hobby drone stick layout).
/// Gamepad and keyboard both work, at the same time if needed — they're
/// merged into the same two virtual sticks.
///
///   Gamepad:   LEFT STICK Y = throttle   | LEFT STICK X  = yaw
///              RIGHT STICK Y = pitch     | RIGHT STICK X = roll
///   Keyboard:  Space/Left Ctrl = throttle up/down
///              Q / E           = yaw left/right
///              W / S           = pitch forward/back
///              A / D           = roll left/right
///
/// REQUIREMENTS:
/// 1. Install the "Input System" package (Window > Package Manager).
/// 2. Project Settings > Player > Active Input Handling: set to
///    "Input System Package (New)" or "Both", then restart the editor.
///
/// SETUP:
/// 1. Put this on your drone body (must have a Rigidbody).
/// 2. Create 4 empty child GameObjects at each propeller position
///    (front-left, front-right, back-left, back-right) and assign them below.
/// 3. Tune values in the Inspector for your drone's mass/scale.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DronMovementAccurate : MonoBehaviour
{
    [Header("Motor positions (empty GameObjects at each propeller)")]
    public Transform motorFrontLeft;
    public Transform motorFrontRight;
    public Transform motorBackLeft;
    public Transform motorBackRight;

    [Header("Power")]
    [Tooltip("Max upward force (Newtons) a single motor can produce. " +
             "Should be comfortably more than (mass * gravity) / 4 so the drone has climb headroom.")]
    public float maxThrustPerMotor = 6f;

    [Header("Altitude Hold (throttle = desired vertical speed, not raw thrust)")]
    [Tooltip("Max climb/descend speed in m/s at full stick deflection")]
    public float maxVerticalSpeed = 3f;
    [Tooltip("How aggressively it corrects vertical speed error (higher = snappier, too high = jittery)")]
    public float verticalP = 12f;

    [Header("Control Sensitivity")]
    [Tooltip("Extra force (N) added/removed per motor at full pitch/roll deflection")]
    public float pitchPower = 1.5f;
    public float rollPower = 1.5f;
    public float yawPower = 0.6f;

    [Header("Attitude Stabilization (PD controller)")]
    [Tooltip("How hard it corrects tilt error")]
    public float levelP = 8f;
    [Tooltip("How hard it resists rotation speed (prevents oscillation)")]
    public float levelD = 1.5f;
    [Tooltip("Max tilt angle in degrees when stick is fully deflected")]
    public float maxTiltAngle = 35f;

    [Header("Stick Feel")]
    [Tooltip("Deadzone applied to each stick axis to avoid drift from imprecise centering")]
    [Range(0f, 0.2f)]
    public float stickDeadzone = 0.05f;

    Rigidbody rb;
    float throttleInput, pitchInput, rollInput, yawInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    void Update()
    {
        Vector2 leftStick = Vector2.zero;
        Vector2 rightStick = Vector2.zero;

        // --- Gamepad ---
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            leftStick += ApplyDeadzone(gamepad.leftStick.ReadValue());
            rightStick += ApplyDeadzone(gamepad.rightStick.ReadValue());
        }

        // --- Keyboard (mapped onto the same two virtual sticks) ---
        Keyboard kb = Keyboard.current;
        if (kb != null)
        {
            float kbYaw = (kb.qKey.isPressed ? -1f : 0f) + (kb.eKey.isPressed ? 1f : 0f);
            float kbThrottle = (kb.spaceKey.isPressed ? 1f : 0f) + (kb.leftCtrlKey.isPressed ? -1f : 0f);
            float kbRoll = (kb.aKey.isPressed ? -1f : 0f) + (kb.dKey.isPressed ? 1f : 0f);
            float kbPitch = (kb.wKey.isPressed ? 1f : 0f) + (kb.sKey.isPressed ? -1f : 0f);

            leftStick += new Vector2(kbYaw, kbThrottle);
            rightStick += new Vector2(kbRoll, kbPitch);
        }

        // Clamp like a real analog stick so e.g. diagonal keyboard presses
        // can't exceed full deflection.
        leftStick = Vector2.ClampMagnitude(leftStick, 1f);
        rightStick = Vector2.ClampMagnitude(rightStick, 1f);

        // Mode 2 mapping
        throttleInput = leftStick.y;
        yawInput = leftStick.x;

        pitchInput = rightStick.y;
        rollInput = rightStick.x;
    }

    Vector2 ApplyDeadzone(Vector2 stick)
    {
        if (stick.magnitude < stickDeadzone) return Vector2.zero;
        return stick;
    }

    void FixedUpdate()
    {
        ApplyMotorThrust();
        ApplyYaw();
        ApplyAttitudeStabilization();
    }

    void ApplyMotorThrust()
    {
        // --- Altitude hold core ---
        // Convert throttle stick into a DESIRED vertical speed, then apply
        // exactly the thrust needed to reach/hold that speed. At stick-centered,
        // desired speed = 0, so this actively cancels gravity AND any drift.
        float desiredVerticalSpeed = throttleInput * maxVerticalSpeed;
        float currentVerticalSpeed = rb.linearVelocity.y;
        float speedError = desiredVerticalSpeed - currentVerticalSpeed;

        float hoverThrust = rb.mass * -Physics.gravity.y;     // total N needed to cancel gravity
        float correction = speedError * verticalP;            // extra/less N to chase desired speed
        float totalThrust = Mathf.Max(0f, hoverThrust + correction);

        float perMotorBase = totalThrust / 4f;

        // X-quad mixer: pitch/roll redistribute thrust between motors on top of the base hover thrust.
        float fl = perMotorBase + pitchInput * pitchPower - rollInput * rollPower;
        float fr = perMotorBase + pitchInput * pitchPower + rollInput * rollPower;
        float bl = perMotorBase - pitchInput * pitchPower - rollInput * rollPower;
        float br = perMotorBase - pitchInput * pitchPower + rollInput * rollPower;

        ApplyMotor(motorFrontLeft, fl);
        ApplyMotor(motorFrontRight, fr);
        ApplyMotor(motorBackLeft, bl);
        ApplyMotor(motorBackRight, br);
    }

    void ApplyMotor(Transform motor, float forceNewtons)
    {
        forceNewtons = Mathf.Clamp(forceNewtons, 0f, maxThrustPerMotor);
        Vector3 force = transform.up * forceNewtons;
        // Applying force AT the motor's position (not the body center) is what
        // naturally generates pitch/roll torque — this is the key physics trick.
        rb.AddForceAtPosition(force, motor.position, ForceMode.Force);
    }

    void ApplyYaw()
    {
        // Real quads yaw via motor spin-direction reaction torque, not tilt —
        // so we apply it directly around the drone's own vertical axis.
        rb.AddRelativeTorque(Vector3.up * yawInput * yawPower, ForceMode.Force);
    }

    void ApplyAttitudeStabilization()
    {
        // Desired orientation: current yaw, but tilted by pitch/roll input.
        Quaternion desiredTilt = Quaternion.Euler(
            pitchInput * maxTiltAngle,
            transform.eulerAngles.y,
            -rollInput * maxTiltAngle
        );

        Quaternion error = desiredTilt * Quaternion.Inverse(transform.rotation);
        error.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f) angle -= 360f;

        Vector3 correctiveTorque = axis.normalized * angle * Mathf.Deg2Rad * levelP;
        Vector3 dampingTorque = -rb.angularVelocity * levelD;

        rb.AddTorque(correctiveTorque + dampingTorque, ForceMode.Force);
    }
}