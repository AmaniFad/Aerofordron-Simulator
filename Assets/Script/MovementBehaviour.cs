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
        rb.AddForce(movementDirection.normalized * speed * Time.deltaTime , ForceMode.Force);
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
        rb.AddForce(new Vector3(0f,-Physics.gravity.y,0f));
    }

    public void MoveRB3D(Vector3 input)
    {
        input.y = 0;
        Vector3 velocityXZ = input.normalized * speed;
        rb.velocity = new Vector3(velocityXZ.x, rb.velocity.y, velocityXZ.z);
    }
    public void RunRB(Vector3 input,float runMultiplier)
    {

        input.y = 0;
        Vector3 velocityXZ = input.normalized * speed * runMultiplier;
        rb.velocity = new Vector3(velocityXZ.x, rb.velocity.y, velocityXZ.z);
    }
    public void Deceleration(float deceleration)
    {
        speed -= deceleration;

        // Limita la velocidad a 0 para evitar que el objeto se mueva hacia atrás
        speed = Mathf.Max(speed, 0);

        // Aplica la velocidad al objeto
        rb.velocity = transform.forward * speed;
    }
}
