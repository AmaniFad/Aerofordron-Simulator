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
    void Start()
    {
        Instance = this;
    }

    public void OnMove(InputValue move)
    {
        playerInput = move.Get<Vector2>();
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
}
