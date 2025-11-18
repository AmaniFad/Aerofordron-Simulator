using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Usamos Addforce para hacer mas realistas las fisicas
    public void Move(Vector3 movementDirection)
    {
            rb.AddForce(movementDirection.normalized * this.speed * Time.deltaTime, ForceMode.Force);


    }
    public void Move(Vector3 movementDirection, float objectSpeed)
    {
               
            rb.AddForce(movementDirection.normalized * objectSpeed * Time.deltaTime, ForceMode.Force);
    }

    public void MoveDronAuto(Vector3 movementDirection, float speedAuto)
    {
        rb.AddForce(movementDirection.normalized * speedAuto * Time.deltaTime, ForceMode.Force);
    }
    public void MoveDronGost(Vector3 movementDirection)
    {
        movementDirection.Normalize();
        transform.position = movementDirection * speed * Time.deltaTime;
    }

    public void MoveWithoutSpeed(Vector3 movementDirection)
    {
        rb.AddForce(movementDirection * Time.deltaTime, ForceMode.Force);

    }
    public void Rotate(Vector3 rotation)
    {
        rb.gameObject.transform.Rotate(rotation * rotationSpeed);
    }

    //Esto es para que haga hover cuando lo dejes quieto
    public void StopMovingOnY()
    {
        rb.AddForce(new Vector3(0f, -Physics.gravity.y, 0f));
    }

    public void StopMoving()
    {
        Vector3 velocity = new Vector3(0, Physics.gravity.y, 0);
        rb.linearVelocity = velocity;

    }

    public void MoveRB3D(Vector3 input)
    {
        input.y = 0;
        Vector3 velocityXZ = input.normalized * speed;
        rb.linearVelocity = new Vector3(velocityXZ.x, rb.linearVelocity.y, velocityXZ.z);
    }
    public void RunRB(Vector3 input, float runMultiplier)
    {

        input.y = 0;
        Vector3 velocityXZ = input.normalized * speed * runMultiplier;
        rb.linearVelocity = new Vector3(velocityXZ.x, rb.linearVelocity.y, velocityXZ.z);
    }
    public void Deceleration(float deceleration)
    {
        speed -= deceleration;
        speed = Mathf.Max(speed, 0);
        rb.linearVelocity = transform.forward * speed;
    }

    [System.Obsolete]
    public void nonInputInputls(Vector3 vector, float impuls)
    {
        rb.velocity = Vector3.Lerp(rb.velocity, vector, Time.deltaTime * impuls);
    }
}
