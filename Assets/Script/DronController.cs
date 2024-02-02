using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronController : MonoBehaviour
{

    private MovementBehaviour mMovementBehaviour;
    void Start()
    {
        mMovementBehaviour = GetComponent<MovementBehaviour>();
    }

    void Update()
    {

        
    }

    private void FixedUpdate()
    {

        Vector2 inputDirection = InputController.Instance.GetDirectionInput();
        float verticalDirection = InputController.Instance.GetVerticalInput();
        Vector3 direction = new Vector3(inputDirection.x, verticalDirection, inputDirection.y);
        mMovementBehaviour.Move(direction);
        mMovementBehaviour.Rotate(InputController.Instance.GetRotationalInput());
    }




}
