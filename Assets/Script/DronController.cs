using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class DronController : MonoBehaviour
{
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    private bool isDronPlatformOn;
    private MovementBehaviour mMovementBehaviour;
    [SerializeField] private float batteryTime;
    void Start()
    {
        mMovementBehaviour = GetComponent<MovementBehaviour>();
    }

    private void FixedUpdate()
    {
        batteryTime -= Time.deltaTime;
        if (batteryTime > 0)
        {

        
        Vector2 inputDirection = InputController.Instance.GetDirectionInput();
        float verticalDirection = InputController.Instance.GetVerticalInput();
        if (transform.position.y >= maxHeight)
        {
            verticalDirection = 0;
        }
        if (verticalDirection == 0)
        {
          mMovementBehaviour.StopMovingOnY();
        }


        Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.y;
        Debug.Log("direction " + transform.right);

        mMovementBehaviour.Move(new Vector3(direction.x,verticalDirection,direction.z));
            mMovementBehaviour.Rotate(InputController.Instance.GetRotationalInput());
        }
    }




}
