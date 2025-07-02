using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows;

public class MoveJoystick : MonoBehaviour
{
    [SerializeField] private Transform leftStickVisual;
    [SerializeField] private Transform rightStickVisual;
    [SerializeField] private float movementRange;
    [SerializeField] private float tiltAngle;
    private Vector3 leftInitialPos;
    private Vector3 rightInitialPos;
    private Quaternion rightInitialRotation;
    private Quaternion leftInitialRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftInitialPos = leftStickVisual.localPosition;
        rightInitialPos = rightStickVisual.localPosition;
        rightInitialRotation = rightStickVisual.rotation;
        leftInitialRotation = leftStickVisual.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStick(leftStickVisual,leftInitialRotation,leftInitialPos,new Vector2(DronInputController.Instance.GetRotationalInput(),  DronInputController.Instance.GetVerticalInput()));
        UpdateStick(rightStickVisual, rightInitialRotation,rightInitialPos,new Vector2(DronInputController.Instance.GetDirectionInput().x, DronInputController.Instance.GetDirectionInput().y));


    }

    private void UpdateStick(Transform stick,Quaternion initialRotation, Vector3 initialPos, Vector2 input)
    {
        float angle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
        //Quaternion tilt = Quaternion.Euler(initialRotation.x + angle, initialRotation.y + angle,initialRotation.z);
        //stick.localRotation = tilt;
        stick.localPosition = Vector3.Lerp(stick.localPosition, initialPos + new Vector3(input.y, 0, input.x) * movementRange, 0.3f);
    }
}
