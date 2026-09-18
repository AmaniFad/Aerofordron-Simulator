using UnityEngine;
using UnityEngine.UIElements;

public class RacingDron : MonoBehaviour
{
    [SerializeField] private float tiltForce;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] AnimationCurve acceleration;
    float t;
    [SerializeField] private float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryToMoveDron()
    {
        Vector2 inputDirection = DronInputController.Instance.GetDirectionInput();
        float verticalDirection = DronInputController.Instance.GetVerticalInput();
        if (verticalDirection < -0.2f || verticalDirection > 0.05f || inputDirection.x < -0.2f || inputDirection.y < -0.2f || inputDirection.x > 0.2f || inputDirection.y > 0.2f)
        {
                Move(new Vector3(inputDirection.x, verticalDirection, inputDirection.y));
        }
        Tilt();
    }
    private void FixedUpdate()
    {
        TryToMoveDron();
    }
    public void Move(Vector3 direction)
    {
        Vector3 horizontalDirection = transform.right * direction.x + transform.forward * direction.z;
        Vector3 verticalDirection = new Vector3(0,direction.y,0);
        if (direction != Vector3.zero)
        {
            t += Time.deltaTime;
        }
        else {
            t -= Time.deltaTime;
        }
        t = Mathf.Clamp01(t);
        direction.Normalize();
        rb.linearVelocity = ((horizontalDirection + verticalDirection) * speed * acceleration.Evaluate(t));

    }

    public void Tilt()
    {
        float currentYRotation = transform.rotation.eulerAngles.y;


        float rotation = DronInputController.Instance.GetRotationalInput();
        print(rotation);
        float tiltAroundZ = -rb.linearVelocity.x * tiltForce;

        float tiltAroundX = +rb.linearVelocity.z * tiltForce;


        Quaternion targetRotation = Quaternion.Euler(tiltAroundX, currentYRotation, tiltAroundZ);

        targetRotation *= Quaternion.Euler(0, rotation * rotationSpeed, 0);

        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 0.5f));
    }
    public void Rotate(float rotation)
    {
        
    }
}
