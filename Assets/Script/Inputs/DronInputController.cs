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

    public float GetVerticalInput()
    {
        return verticalInput;
    }

    public float GetRotationalInput()
    {
        return rotationalInput;
    }

    public Vector2 GetDirectionInput()
    {
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
