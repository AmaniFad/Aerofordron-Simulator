using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows;

public class DronController : MonoBehaviour
{
    [Header("Distances")]
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private float groundedRayDistance;
    private bool isDronPlatformOn;
    private bool canMove;
    private MovementBehaviour mMovementBehaviour;
    private bool isPlaying;
    [Header("References")]
    [SerializeField] private EventReference soundReference;
    private FMOD.Studio.EventInstance helixSound;
    private StudioEventEmitter eventEmitter;
    [Header("Rotations")]
    [SerializeField] private float tiltAngle;
    [SerializeField] private float rotationSpeed;
    private bool isGrounded;
    //POR IMPLEMENTAR
    //[SerializeField] private GameObject playerOnGroundFeedback;
    void Start()
    {
        eventEmitter = GetComponent<StudioEventEmitter>();
        isPlaying = false;
        mMovementBehaviour = GetComponent<MovementBehaviour>();
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundedRayDistance);
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (!isPlaying)
            {
                PlayDroneSound();
                isPlaying = true;
            }
            TryToMoveDron();
        }
        else
        {
            StopPlayDroneSound();
        }
    }

    private void TryToMoveDron()
    {
        Vector2 inputDirection = DronInputController.Instance.GetDirectionInput();
        float verticalDirection = DronInputController.Instance.GetVerticalInput();
        if (transform.position.y >= maxHeight)
        {
            verticalDirection = 0;
        }
        if (verticalDirection == 0)
        {
            mMovementBehaviour.StopMovingOnY();
        }
        mMovementBehaviour.Move(new Vector3(0, verticalDirection,0));

        if (!CheckIfGrounded())
        {
            Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.y;

            mMovementBehaviour.Move(new Vector3(direction.x, 0, direction.z));
            SendDronRotation(inputDirection);
        }
        //if (CheckIfGrounded() && inputDirection != Vector2.zero)
        //{
        //    playerOnGroundFeedback.SetActive(true);
        //}
        //else
        //{
        //    playerOnGroundFeedback.SetActive(false);
        //}
    }

    private void SendDronRotation(Vector2 inputDirection)
    {
        //Necesario sino vuelve a 0 la rotation para los lados el momento que dejes de pulsar
        float currentYRotation = transform.rotation.eulerAngles.y;

        //Esto es para que tire un poco hacia el lado que se esta moviendo
        float tiltAroundZ = -inputDirection.x * tiltAngle;
        float tiltAroundX = +inputDirection.y * tiltAngle;


        Quaternion targetRotation = Quaternion.Euler(tiltAroundX, currentYRotation, tiltAroundZ);

        // Aqui se pone la rotacion Recordatorio no utilizar time.DeltaTime en un fixedUpdate
        float additionalRotationY = DronInputController.Instance.GetRotationalInput() * rotationSpeed;
        targetRotation *= Quaternion.Euler(0, additionalRotationY, 0);

        // Apply the rotation with slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

    }

    public void StartDron()
    {
        PlayerReferences.instance.SetDron(gameObject);
        PlayerStateController.instance.CameraToDron(gameObject);
        PlayerStateController.instance.StopMoving();
        canMove = true;
        PlayerReferences.instance.GetHUD().SetActive(true);
    }

    public void StopDron()
    {
        PlayerReferences.instance.SetDron(new GameObject());
        PlayerStateController.instance.CameraToDron(null);
        PlayerStateController.instance.ResumeMoving();
        canMove = false;
        PlayerReferences.instance.GetHUD().SetActive(false);
    }

    private void PlayDroneSound()
    {

        eventEmitter.EventReference = soundReference;
        eventEmitter.Play();

    }

    private void StopPlayDroneSound()
    {
        eventEmitter.EventReference = soundReference;
        eventEmitter.Stop();
    }
}


