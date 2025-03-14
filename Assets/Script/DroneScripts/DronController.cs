using FMODUnity;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class DronController : MonoBehaviour
{
    [Header("Distances")]
    public static DronController dronInstance;
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private float groundedRayDistance;
    private bool isDronPlatformOn;
    [SerializeField] private bool canMove;
    private MovementBehaviour mMovementBehaviour;
    private bool isPlaying;
    [Header("References")]
    [SerializeField] private EventReference soundReference;
    private FMOD.Studio.EventInstance helixSound;
    private StudioEventEmitter eventEmitter;
    [Header("Rotations")]
    [SerializeField] private float tiltAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject dronView;
    private float maxUpTilt = 550;
    private float maxDownTilt = -50;
    private float currentCameraTilt;
    [SerializeField] private float cameraMovementSpeed;
    private bool isGrounded;
    private Rigidbody rb;
    private float lastVerticalInput;
    //POR IMPLEMENTAR
    //[SerializeField] private GameObject playerOnGroundFeedback;
    void Start()
    {
        dronInstance = this;
        rb = GetComponent<Rigidbody>();
        currentCameraTilt = 0;
        eventEmitter = GetComponent<StudioEventEmitter>();
        isPlaying = false;
        tiltAngle = 30;
        mMovementBehaviour = GetComponent<MovementBehaviour>();
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundedRayDistance);
    }
    private void Update()
    {
        if (DisplayInputData.cameraUP)
        {
            MoveCameraUp();
        }
        if (DisplayInputData.cameraDown)
        {
            MoveCameraDown();
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            TryToMoveDronHorizontally();
        }

    }
    private void LateUpdate()
    {
        if (canMove)
        {
            if (!isPlaying)
            {
                PlayDroneSound();
                isPlaying = true;
            }
            TryToMoveDronVertically();
        }
        else
        {
            StopPlayDroneSound();
        }
        Vector2 inputDirection = DisplayInputData.rightControllerDirection;
        //DoDroneTilt(inputDirection);
    }
    private void TryToMoveDronVertically()
    {
        float verticalDirection = DisplayInputData.leftControllerDirection.y;
        if (verticalDirection > -0.2f && verticalDirection < 0.2f)
        {
            verticalDirection = 0;
        }


        //if (transform.position.y >= maxHeight)
        //{
        //    verticalDirection = 0;
        //}


        print("Direccion: " + verticalDirection);
        mMovementBehaviour.Move(new Vector3(0, verticalDirection, 0));



        if (verticalDirection == 0)
        {
            rb.useGravity = false;
            //mMovementBehaviour.StopMovingOnY();
        }
        float cameraMovement = DronInputController.Instance.GetCameraMovement();
 


        //if (CheckIfGrounded() && inputDirection != Vector2.zero)
        //{
        //    playerOnGroundFeedback.SetActive(true);
        //}
        //else
        //{
        //    playerOnGroundFeedback.SetActive(false);
        //}
    }

    //public void TiltCamera()
    //{

    //}
    public void TryToMoveDronHorizontally()
    {
        Vector2 inputDirection = DisplayInputData.rightControllerDirection;
        if (inputDirection.x > -0.2f && inputDirection.x < 0.2f)
        {
            inputDirection.x = 0;
        }
        if (inputDirection.y > -0.2f && inputDirection.y < 0.2f)
        {
            inputDirection.y = 0;
        }

        if (!CheckIfGrounded())
        {
            Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.y;

            mMovementBehaviour.Move(new Vector3(direction.x, 0, direction.z));
            SendDronRotation(inputDirection);
            if (WindControlller.Instance != null)
            {
                mMovementBehaviour.MoveWithoutSpeed(WindControlller.Instance.GetWindForce());
            }
        }
    }

    private void OnDestroy()
    {
        StopPlayDroneSound();
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

    private void DoDroneTilt(Vector2 inputDirection)
    {
        float currentYRotation = transform.rotation.eulerAngles.y;

        //Esto es para que tire un poco hacia el lado que se esta moviendo
        float tiltAroundZ = -inputDirection.x * tiltAngle;
        float tiltAroundX = +inputDirection.y * tiltAngle;

        Quaternion targetRotation = Quaternion.Euler(tiltAroundX, currentYRotation, tiltAroundZ);

        // Aqui se pone la rotacion Recordatorio no utilizar time.DeltaTime en un fixedUpdate

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

        
    }
    private void MoveCameraUp()
    {
        
            if (currentCameraTilt < maxUpTilt)
            {
                Quaternion rotation = gameObject.transform.rotation;
                rotation.x += 1 * Time.deltaTime * cameraMovementSpeed;
                Debug.Log("Rotation " + rotation);
                currentCameraTilt += (float)10;
                dronView.transform.Rotate(new Vector3(rotation.x, 0, 0), rotation.x * 10, Space.Self);
            }

        
    }

    private void MoveCameraDown()
    {
        if (currentCameraTilt > maxDownTilt)
        {

            Quaternion rotation = gameObject.transform.rotation;
            rotation.x += -1 * Time.deltaTime * cameraMovementSpeed;
            Debug.Log("Rotation " + rotation);
            currentCameraTilt -= (float)10;
            dronView.transform.Rotate(new Vector3(-rotation.x, 0, 0), rotation.x * 10, Space.Self);
        }
    }
    public void StartDron()
    {
        PlayerReferences.instance.SetDron(gameObject);
        PlayerStateController.instance.CameraToDron(gameObject);
        PlayerStateController.instance.StopMoving();
        StartCoroutine(DronCanMove());
        GetComponent<Animator>().SetBool("flying", true);
        PlayerReferences.instance.GetHUD().SetActive(true);
    }

    private IEnumerator DronCanMove()
    {
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }
    public void StopDron()
    {
        PlayerReferences.instance.SetDron(null);
        PlayerStateController.instance.CameraToDron(null);
        PlayerStateController.instance.ResumeMoving();
        canMove = false;
        GetComponent<Animator>().SetBool("flying", false);
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

    public void StopMovingDron()
    {
        canMove = false;
    }

    public void StartMovingDron()
    {
        canMove = true;
    }

    public bool CanMoveDron()
    {
        return canMove;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
}


