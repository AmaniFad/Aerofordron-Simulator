using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance;
    private float verticalInput;
    private float rotationalInput;
    private Vector2 directionInput;

    private void Start()
    {
        Instance = this;
    }
    public void OnDroneRightStick(InputValue inputValue)
    {

        directionInput = inputValue.Get<Vector2>();
        Debug.Log(directionInput);
    } 

    public void OnDroneLeftStick(InputValue inputValue)
    {

        verticalInput = inputValue.Get<Vector2>().y;
        rotationalInput = inputValue.Get<Vector2>().x;
        Debug.Log(verticalInput + " " + rotationalInput);

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
}
