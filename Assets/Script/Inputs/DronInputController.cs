using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DronInputController : MonoBehaviour
{
    public static DronInputController Instance;
    private float verticalInput;
    private float rotationalInput;
    private Vector2 directionInput;
    private bool changeCamera;
    private float cameraMovement;
    private void Start()
    {
        Instance = this;
    }
    public void OnDroneRightStick(InputValue inputValue)
    {
        directionInput = inputValue.Get<Vector2>();
        Debug.Log("VerticalInput " + verticalInput);
    }

    public void OnDroneLeftStick(InputValue inputValue)
    {

        verticalInput = inputValue.Get<Vector2>().y;
        Debug.Log("VerticalInput " + verticalInput);
        rotationalInput = inputValue.Get<Vector2>().x;

    }

    public void OnMoveCamera(InputValue inputValue)
    {
        cameraMovement = inputValue.Get<Vector2>().y;
    }

    public float GetCameraMovement()
    {
        return cameraMovement;
    }
    public float GetVerticalInput()
    {
        if (verticalInput > -0.2f && verticalInput < 0.1f)
        {
            verticalInput = 0;
        }
            return verticalInput;
    }

    public float GetRotationalInput()
    {
        if (rotationalInput > -0.2f && rotationalInput < 0.1f)
        {
            rotationalInput = 0;
        }
        return rotationalInput;
    }

    public Vector2 GetDirectionInput()
    {
        if (directionInput.x > -0.1f && directionInput.x < 0.1f)
        {
            directionInput.x = 0;
        }
        if (directionInput.y > -0.1f && directionInput.y < 0.1f)
        {
            directionInput.y = 0;
        }
        return directionInput;
    }

    public void OnChangeCamera(InputValue value)
    {
        if (value.isPressed)
        {
            changeCamera = true;
        }
    }

    public void HasChangedCamera()
    {
        changeCamera = false;
    }

    public bool CanChangeCamera()
    {
        return changeCamera;
    }
}
