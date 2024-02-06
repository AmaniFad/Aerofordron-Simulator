using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance;
    private Vector2 playerInput;

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
   
}
