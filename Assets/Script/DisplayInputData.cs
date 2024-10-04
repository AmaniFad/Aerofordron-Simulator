using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

[RequireComponent(typeof(InputData))]
public class DisplayInputData : MonoBehaviour
{
    public TextMeshProUGUI leftScoreDisplay;
    public TextMeshProUGUI rightScoreDisplay;

    private InputData _inputData;
    private float _leftMaxScore = 0f;
    private float _rightMaxScore = 0f;
    static public Vector2 leftControllerDirection;
    static public Vector2 rightControllerDirection;
    static public bool isPrimaryPressed;
    static public bool isChangeCameraPressed;
    static public bool isMenuPressed;
    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 direction))
        {
            leftControllerDirection = direction;
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightDirection))
        {
            rightControllerDirection = rightDirection;
        }


        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            isChangeCameraPressed = isPressed;
        }
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool pres))
        {
            isPrimaryPressed = pres;
        }


        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonPress1))
        {
           isMenuPressed = menuButtonPress1;
        }
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool menuButtonPress2))
        {
            isMenuPressed = menuButtonPress2;
        }

    }
}
