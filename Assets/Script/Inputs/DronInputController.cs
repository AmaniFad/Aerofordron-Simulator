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
    private float aguaInput;
    private bool buttonPressedAgua = false;
    private bool buttonPressedMode = false;
    private bool isRemoteDron;
    private float isModeAtti;
    private void Start()
    {
        Instance = this;
    }
    public void OnDroneRightStick(InputValue inputValue)
    {
        directionInput = inputValue.Get<Vector2>();
    }

    public void OnDroneLeftStick(InputValue inputValue)
    {

        verticalInput = inputValue.Get<Vector2>().y;
        rotationalInput = inputValue.Get<Vector2>().x;

    }

    public void OnMoveCamera(InputValue inputValue)
    {
        cameraMovement = inputValue.Get<Vector2>().y;
    }
    public void OnAgua(InputValue value)
    {
        if (value.isPressed && !buttonPressedAgua)
        {
            aguaInput = (aguaInput == 0) ? 1 : 0;
            buttonPressedAgua = true;
        }
        else if (!value.isPressed)
        {
            buttonPressedAgua = false;
        }
    }
    public void OnModeDron(InputValue value)
    {
        if (value.isPressed && !buttonPressedMode) 
        {
            isModeAtti = (isModeAtti == 0) ? 1 : 0;
            buttonPressedMode = true;
        }
        else if(value.isPressed) 
        {
            buttonPressedMode = false;
        }
    }
    public float GetModeAtti()
    {
        return isModeAtti;
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
    public void OnRemoteDron(InputValue value)
    {
        if (value.isPressed)
        {
            isRemoteDron = true;
        }
        else
        {
            isRemoteDron = false;
        }
    }
    public bool GetRemoteDron()
    {
        return isRemoteDron;
    }
    public void HasChangedCamera()
    {
        changeCamera = false;
    }

    public bool CanChangeCamera()
    {
        return changeCamera;
    }
    public float GetAguaInput()
    {
        return aguaInput;
    }
}
