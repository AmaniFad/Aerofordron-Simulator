using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    private MovementBehaviour MB;
    private Rigidbody rb;

    [SerializeField] private float power = 5;
    [SerializeField] private float torque = 0.5f;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private Vector2 movementVector;
    private void Awake()
    {
        MB = GetComponent<MovementBehaviour>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < maxSpeed)
        {
            MB.MovementCar(movementVector, power);
        }
        MB.TroqueCar(movementVector, torque);
    }
    public void SetMove(Vector2 movmentInput)
    {
        this.movementVector = movmentInput;
    }
    
}
