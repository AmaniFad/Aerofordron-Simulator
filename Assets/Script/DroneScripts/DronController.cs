using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Windows;
using VInspector;

public class DronController : MonoBehaviour
{
    #region variables
    [Header("Distances")]
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private float groundedRayDistance;
    private bool isDronPlatformOn;
    private bool canMove;
    private MovementBehaviour mMovementBehaviour;
    private bool isPlaying;
    [SerializeField] private float maxUpTilt;
    [SerializeField] private float maxDownTilt;
    private float currentCameraTilt;
    [Header("References")]
    [SerializeField] private EventReference soundReference;
    [SerializeField] private GameObject dronVisuals;
    [SerializeField] private DronMode[] dronModes;
    public event Action<int> onDronModeChange;
    private int _currentDronMode;
    private int currentDronMode
    {
        get => _currentDronMode;
        set
        {
            _currentDronMode = value;
            onDronModeChange?.Invoke(currentDronMode);
        }
    }
    private FMOD.Studio.EventInstance helixSound;
    private StudioEventEmitter eventEmitter;
    [Header("Rotations")]
    [SerializeField] private float tiltAngle;
    [SerializeField] private GameObject dronView;
    [SerializeField] private float maxDronViewRotation;
    [SerializeField] private float minDronViewRotation;
    private float currentCameraRotationSimplified;
    private Rigidbody rb;
    float currentZTiltMultiplier = 1;

    [SerializeField] private float cameraMovementSpeed;
    private bool isGrounded;
    private float lastTiltZ;

    [Header("SpawnPoints")]
    [SerializeField] private Transform waypoints;

    private bool remoteDron;
    private bool aux;
    private float _attiMode;
    public bool _isLoaded;

    [System.Serializable]
    private class DronMode
    {
        public float rotationSpeed;
        public float speed;
    }


    public float GetAttiMode()
    {
        return _attiMode;
    }
    //POR IMPLEMENTAR
    //[SerializeField] private GameObject playerOnGroundFeedback;
    #endregion
    void Start()
    {

        currentDronMode = 1;
        rb = GetComponent<Rigidbody>();
        currentCameraTilt = 0;
        eventEmitter = GetComponent<StudioEventEmitter>();
        isPlaying = false;
        mMovementBehaviour = GetComponent<MovementBehaviour>();
        PlayerReferences.instance.SetDron(this.gameObject);
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundedRayDistance);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    private void Update()
    {
        if (eventEmitter)
        {
            eventEmitter.SetParameter("Speed", Mathf.Abs((rb.linearVelocity.x + rb.linearVelocity.y) / 2 / 60 * 10));
            eventEmitter.SetParameter("Volume", Mathf.Abs((rb.linearVelocity.x + rb.linearVelocity.y) / 2 / 60 * 10));
        }
        if (DronInputController.Instance.GetCameraMovement() > 0)
        {
            MoveCameraDown();
        }
        if (DronInputController.Instance.GetCameraMovement() < 0)
        {
            MoveCameraUp();
        }
        if (DronInputController.Instance.GetModeChange()){
            ChangeMode();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (!isPlaying)
            {
                PlayDroneSound();
                isPlaying = true;
            }
            if (!remoteDron)
            {
                TryToMoveDron();
            }

            if (!eventEmitter.IsPlaying())
            {
                eventEmitter.Play();
            }
            returnToSpawn();
        }
        else
        {
            StopPlayDroneSound();
        }
    }

    private void LateUpdate()
    {

    }

    [Obsolete]
    private void TryToMoveDron()
    {
        Vector2 inputDirection = DronInputController.Instance.GetDirectionInput();
        float verticalDirection = DronInputController.Instance.GetVerticalInput();

        if (transform.position.y >= maxHeight)
        {
            verticalDirection = 0;
        }
        if (verticalDirection == 0)
        {
            GetComponent<Rigidbody>().useGravity = false;
            //mMovementBehaviour.StopMovingOnY();
        }
        if (verticalDirection < -0.2f || verticalDirection > 0.05f)
        {
            if(_isLoaded)
                mMovementBehaviour.MoveWithLoad(new Vector3(0, verticalDirection, 0), dronModes[currentDronMode].speed);
            else
                mMovementBehaviour.Move(new Vector3(0, verticalDirection, 0), dronModes[currentDronMode].speed);
        }
            

        _attiMode = DronInputController.Instance.GetModeAtti();
        //Debug.Log(CheckIfGrounded());
        if (!CheckIfGrounded())
        {
            Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.y;

            if (_attiMode == 1)
            {
                if (inputDirection.magnitude > 0.01f)
                {
                    if (_isLoaded)
                    {
                        mMovementBehaviour.MoveWithLoad(new Vector3(direction.x, 0, direction.z), dronModes[currentDronMode].speed);
                    }
                    else 
                    {
                        mMovementBehaviour.Move(new Vector3(direction.x, 0, direction.z), dronModes[currentDronMode].speed);
                    }

                        SendDronRotation(inputDirection);
                }
                else
                {
                    // Sueltas los sticks → que decaiga lentamente la velocidad
                    mMovementBehaviour.nonInputInputls(new Vector3(0, rb.linearVelocity.y, 0), 0.05f);
                }
                if (GetComponent<WindObject>() != null)
                {
                    //mMovementBehaviour.MoveWithoutSpeed(WindControlller.Instance.GetWindForce());
                    GetComponent<WindObject>().SetWindDampen(1f);
                }
            }
            else // GPS Mode
            {
                GetComponent<WindObject>().SetWindDampen(0f);

                if (inputDirection.magnitude > 0.01f)
                {
                    if (_isLoaded)
                    {
                        mMovementBehaviour.MoveWithLoad(new Vector3(direction.x, 0, direction.z), dronModes[currentDronMode].speed);
                    }
                    else
                    {
                        mMovementBehaviour.Move(new Vector3(direction.x, 0, direction.z), dronModes[currentDronMode].speed);
                    }
                    SendDronRotation(inputDirection);
                }
                else
                {
                    if(_isLoaded)
                        mMovementBehaviour.nonInputInputls(rb.linearVelocity, 0.2f);
                    else
                        mMovementBehaviour.nonInputInputls(Vector3.zero, 5f);
                }
            }
            SendDronRotation(inputDirection);
        }

        _attiMode = 0;
        float cameraMovement = DronInputController.Instance.GetCameraMovement();

    }
    // consume mas en modo atti
    private void OnDestroy()
    {
        StopPlayDroneSound();
    }
    private void SendDronRotation(Vector2 inputDirection)
    {
        //Necesario sino vuelve a 0 la rotation para los lados el momento que dejes de pulsar
        float currentYRotation = transform.rotation.eulerAngles.y;
        if (lastTiltZ != -inputDirection.x * tiltAngle && lastTiltZ != 0)
        {
            currentZTiltMultiplier = 2;
        }
        //Esto es para que tire un poco hacia el lado que se esta moviendo
        float tiltAroundZ = -inputDirection.x * tiltAngle * currentZTiltMultiplier;

        float tiltAroundX = +inputDirection.y * tiltAngle;
        currentZTiltMultiplier = Mathf.Lerp(currentZTiltMultiplier, 1, 0.15f);
        if (tiltAroundZ != 0)
            lastTiltZ = -inputDirection.x * tiltAngle;

        print("Tilt: " + tiltAroundZ + " Direction " + -inputDirection.x);

        Quaternion targetRotation = Quaternion.Euler(tiltAroundX, currentYRotation, tiltAroundZ);

        // Aqui se pone la rotacion Recordatorio no utilizar time.DeltaTime en un fixedUpdate
        float additionalRotationY = DronInputController.Instance.GetRotationalInput() * dronModes[currentDronMode].rotationSpeed;
        targetRotation *= Quaternion.Euler(0, additionalRotationY, 0);

        // Apply the rotation with slerp
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 5f));

    }

    #region Camera Rotation
    private void MoveCameraUp()
    {

        if (currentCameraTilt < maxUpTilt && canMove)
        {
            float viewRotation = 1 * Time.deltaTime * cameraMovementSpeed;
            currentCameraTilt += 10f;
            dronView.transform.Rotate(viewRotation, 0, 0, Space.Self);
        }
    }

    private void MoveCameraDown()
    {
        if (currentCameraTilt > maxDownTilt && canMove)
        {
            float viewRotation = -1 * Time.deltaTime * cameraMovementSpeed;
            currentCameraTilt -= 10f;
            dronView.transform.Rotate(viewRotation, 0, 0, Space.Self);
        }
    }
    #endregion
    #region Start and Stop dron methods
    public void StartDron()
    {
        PlayerReferences.instance.SetDron(gameObject);
        PlayerStateController.instance.CameraToDron(gameObject);
        PlayerStateController.instance.StopMoving();
        canMove = true;
        GetComponent<Animator>().SetBool("flying", true);
        PlayerReferences.instance.GetHUD().SetActive(false);
        PlayerReferences.instance.SetIsFlyingDrone(true);
    }

    public void StopDron()
    {
        PlayerReferences.instance.SetDron(null);
        PlayerStateController.instance.CameraToDron(null);
        PlayerStateController.instance.ResumeMoving();
        canMove = false;
        GetComponent<Animator>().SetBool("flying", false);
        PlayerReferences.instance.SetIsFlyingDrone(false);
        PlayerReferences.instance.GetHUD().SetActive(true);
    }

    private void PlayDroneSound()
    {

        eventEmitter.EventReference = soundReference;
        eventEmitter.Play();

    }

    private void StopPlayDroneSound()
    {
        eventEmitter.EventReference = soundReference;
        eventEmitter.Stop();
    }

    public void StopMovingDron()
    {
        canMove = false;
    }

    public void StartMovingDron()
    {
        if (!PlayerStateController.instance.CanMove())
            canMove = true;
    }
    #endregion

    private void returnToSpawn()
    {
        if (DronInputController.Instance.GetRemoteDron())
        {
            remoteDron = true;
            Debug.Log("oressed" + remoteDron);
        }
        if (remoteDron)
        {
            GetComponent<WindObject>().SetWindDampen(0f);
            float speed = 750f;
            Transform targetWaypoint = waypoints;


            Vector3 direction = (targetWaypoint.position - transform.position);
            direction.y = 0f;
            direction.Normalize();

            float distanceY = Mathf.Abs(transform.position.y - targetWaypoint.position.y);
            float distanceAll = Vector3.Distance(transform.position, targetWaypoint.position);

            // Distancia horizontal
            float distanceXZ = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z),
                new Vector2(targetWaypoint.position.x, targetWaypoint.position.z)
            );
            if (distanceXZ < 20f)
            {
                speed = 50;
            }
            if (distanceXZ <= 0.3f || aux)
            {
                // Reducir velocidad gradualmente al descender
                speed = 60;
                mMovementBehaviour.MoveDronAuto(Vector3.down, speed);

                // Estabilizar rotación horizontal
                Quaternion stableRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, stableRotation, Time.deltaTime * 2f);

                aux = true;
            }
            else
            {
                // Avanzar horizontalmente
                mMovementBehaviour.MoveDronAuto(direction, speed);

                // Rotación suave mirando hacia adelante
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            }

            if (distanceXZ <= 0.5f && distanceY <= 0.2f)
            {
                mMovementBehaviour.StopMovingOnY();
                remoteDron = false;
                aux = false;
            }
        }
    }

    [Button]
    public void ChangeMode()
    {
        if (currentDronMode + 1 >= dronModes.Length)
        {
            currentDronMode = 0;
        }
        else
        {

            currentDronMode++;
        }
    }

    public int GetCurrentDronMode()
    {
        return currentDronMode;
    }
}
