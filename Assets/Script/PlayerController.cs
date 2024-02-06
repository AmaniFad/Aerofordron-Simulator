using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Transform cameraTransform;
    private MovementBehaviour MB;

    void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        Vector2 playerWasd = PlayerInputController.Instance.GetPlayerInput();

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 input = cameraForward * playerWasd.y + cameraRight * playerWasd.x;
        MB.MoveRB3D(input);
    }

}
