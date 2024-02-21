using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class DronController : MonoBehaviour
{
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    private bool isDronPlatformOn;
    private bool canMove;
    private MovementBehaviour mMovementBehaviour;
    private bool isPlaying;
    [SerializeField] private EventReference soundReference;
    private FMOD.Studio.EventInstance helixSound;
    private StudioEventEmitter eventEmitter;
    [SerializeField] private float tiltAngle;
    void Start()
    {
        eventEmitter = GetComponent<StudioEventEmitter>();
        isPlaying = false;
        mMovementBehaviour = GetComponent<MovementBehaviour>();
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

            Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.y;

            mMovementBehaviour.Move(new Vector3(direction.x, verticalDirection, direction.z));
            mMovementBehaviour.Rotate(DronInputController.Instance.GetRotationalInput());
            float tiltAroundZ = -inputDirection.x * tiltAngle; // Left-right tilt
            float tiltAroundX = +inputDirection.y * tiltAngle; // Forward-backward tilt
            Quaternion targetRotation = Quaternion.Euler(tiltAroundX, 0f, tiltAroundZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        else
        {
            StopPlayDroneSound();
        }
    }

    public void StartDron()
    {
        PlayerReferences.instance.SetDron(gameObject);
        PlayerStateController.instance.CameraToDron(gameObject);
        PlayerStateController.instance.StopMoving();
        canMove = true;
    }

    public void StopDron()
    {
        PlayerReferences.instance.SetDron(new GameObject());
        PlayerStateController.instance.CameraToDron(new GameObject());
        PlayerStateController.instance.ResumeMoving();
        canMove = false;
    }

    private void PlayDroneSound()
    {

        eventEmitter.EventReference = soundReference;
        eventEmitter.Play();

    }

    private void StopPlayDroneSound()
    {
        eventEmitter.Event = "event:/DronHelix";
        eventEmitter.Stop();
    }
}


