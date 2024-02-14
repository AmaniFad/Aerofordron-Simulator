using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class DronController : MonoBehaviour
{
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    private bool isDronPlatformOn;
    private bool canMove;
    private MovementBehaviour mMovementBehaviour;
    void Start()
    {
        mMovementBehaviour = GetComponent<MovementBehaviour>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Vector2 inputDirection = DronInputController.Instance.GetDirectionInput();
            float verticalDirection = DronInputController.Instance.GetVerticalInput();
            if (transform.position.y >= maxHeight)
            {
                verticalDirection = 0;
            }
            if (verticalDirection == 0)
            {
                mMovementBehaviour.StopMovingOnY();
            }

            Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.y;

            mMovementBehaviour.Move(new Vector3(direction.x, verticalDirection, direction.z));
            mMovementBehaviour.Rotate(DronInputController.Instance.GetRotationalInput());
        }
    }

    public void StartDron()
    {
        PlayerStateController.instance.CameraToDron(gameObject);
        PlayerStateController.instance.StopMoving();
        canMove = true;
    }

    public void StopDron()
    {
        PlayerStateController.instance.CameraToDron(new GameObject());
        PlayerStateController.instance.ResumeMoving();
        canMove = false;
    }
}


