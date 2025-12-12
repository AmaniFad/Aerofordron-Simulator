using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance;
    private Vector2 playerInput;
    private bool isInteracting;
    private bool isRunning;
    private bool isPausing;
    private bool fullView;
    private bool isUsingKeyboard;
    private bool isUsingGamepad;
    void Start()
    {
        if(Instance == null)
            Instance = this;
    }

    public void OnMove(InputValue move)
    {
        playerInput = move.Get<Vector2>();
    }

    public void OnDronMode(InputAction.CallbackContext callbackContext)
    {
 
    }


    public Vector2 GetPlayerInput()
    {
        return playerInput;
    }

    public void OnInteract(InputValue inputValue)
    {
        isInteracting = true;
    }
   
    public void HasInteracted()
    {
        isInteracting = false;
    }
    public bool IsInteracting()
    {
        return isInteracting;
    }

    public void OnRun(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }
    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        Debug.Log("Pausa");
        isPausing = true;
    }

    public void HasPaused()
    {
        isPausing = false;
    }
    public bool IsPausing()
    {
        return isPausing;
    }

    public void OnFullView(InputValue value)
    {
        fullView = !fullView;
    }

    public bool IsFullView()
    {
        return fullView;
    }

    public void SetFullView(bool fullView)
    {
        this.fullView = fullView;
    }
    public void OnControlsChanged(PlayerInput playerInput)
    {

        if (playerInput.currentControlScheme.Equals("Keyboard&Mouse"))
        {
            isUsingKeyboard = true;
            isUsingGamepad = false;
            print("Keyboard");
        }
        if (playerInput.currentControlScheme.Equals("Gamepad") || playerInput.currentControlScheme.Equals("Joystick"))
        {
            print("Gamepad");
            isUsingGamepad = true;
            isUsingKeyboard = false;
        }
        else
        {
            Debug.LogWarning("Unknown control scheme: " + playerInput.currentControlScheme);
        }
    }

    public bool IsUsingGamepad()
    {
        return isUsingGamepad;
    }

    public bool IsUsingKeyboard()
    {
        return isUsingKeyboard;
    }
}
