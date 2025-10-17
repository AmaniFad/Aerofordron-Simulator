
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.InputSystem;

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
    static public bool cameraUP;
    static public bool cameraDown;
    [SerializeField] public InputAction tryInput;
    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }
    // Update is called once per frame
    void Update()
    {
        _inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 righ234tDirection);
        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 direction))
        {
            Debug.Log("DisplayInput Left controller " + leftControllerDirection);
            leftControllerDirection = direction;
        }

        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 rightDirection))
        {
            rightControllerDirection = rightDirection;
        }


        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool isPressed))
        {
            isChangeCameraPressed = isPressed;
        }
        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool pres))
        {
            isPrimaryPressed = pres;
        }


        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out bool menuButtonPress1))
        {
           isMenuPressed = menuButtonPress1;
        }
        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool menuButtonPress2))
        {
            isMenuPressed = menuButtonPress2;
            print(isMenuPressed);
        }
        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool cameraUp))
        {
            cameraUP = cameraUp;
        }
        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool isCameraDown))
        {
            cameraDown = isCameraDown;
        }

    }
}
