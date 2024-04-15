using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractFeedbackController : MonoBehaviour
{
    [SerializeField] private GameObject controllerFeedback;
    [SerializeField] private GameObject pcFeedback;
 
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Gamepad: " + PlayerInputController.Instance.IsUsingGamepad() + " Keyboard; " + PlayerInputController.Instance.IsUsingKeyboard());
        if (PlayerInputController.Instance.IsUsingGamepad())
        {
            controllerFeedback.SetActive(true);
            pcFeedback.SetActive(false);
        }
        else if (PlayerInputController.Instance.IsUsingKeyboard())
        {
            controllerFeedback.SetActive(false);
            pcFeedback.SetActive(true);
        }
    }
}
