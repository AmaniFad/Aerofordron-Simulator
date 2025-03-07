using FMODUnity;
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
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private float groundedRayDistance;
    private bool isDronPlatformOn;
    private bool canMove;
    private MovementBehaviour mMovementBehaviour;
    private bool isPlaying;
    private float maxUpTilt = 550;
    private float maxDownTilt = -50;
    private float currentCameraTilt;
    [Header("References")]
    [SerializeField] private EventReference soundReference;
    [SerializeField] private GameObject dronVisuals;
    private FMOD.Studio.EventInstance helixSound;
    private StudioEventEmitter eventEmitter;
    [Header("Rotations")]
    [SerializeField] private float tiltAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject dronView;
    [SerializeField] private float maxDronViewRotation;
    [SerializeField] private float minDronViewRotation;
    private float currentCameraRotationSimplified;
    [SerializeField] private float cameraMovementSpeed;
    private bool isGrounded;
    //POR IMPLEMENTAR
    //[SerializeField] private GameObject playerOnGroundFeedback;
    void Start()
    {
        currentCameraTilt = 0;
        //currentCameraRotationSimplified = 0;
        eventEmitter = GetComponent<StudioEventEmitter>();
        isPlaying = false;
        mMovementBehaviour = GetComponent<MovementBehaviour>();
        tiltAngle = 40;
        rotationSpeed = 150;
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundedRayDistance);
    }
    private void Update()
    {

        if (DronInputController.Instance.GetCameraMovement() > 0)
        {
            MoveCameraUp();
        }
        if (DronInputController.Instance.GetCameraMovement() < 0)
        {
            MoveCameraDown();
        }
        
    }

    //private void FixedUpdate()
    //{
    //    if (canMove)
    //    {
    //        if (!isPlaying)
    //        {
    //            PlayDroneSound();
    //            isPlaying = true;
    //        }
    //        TryToMoveDron();
    //    }
    //    else
    //    {
    //        StopPlayDroneSound();
    //    }
    //}
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

    private void LateUpdate()
    {

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
            GetComponent<Rigidbody>().useGravity = false;
            //mMovementBehaviour.StopMovingOnY();
        }
        if (verticalDirection < -0.2f || verticalDirection > 0.05f)
            mMovementBehaviour.Move(new Vector3(0, verticalDirection, 0));

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
        float cameraMovement = DronInputController.Instance.GetCameraMovement();
        //if (cameraMovement != 0)
        //{
        //    Debug.Log(currentCameraRotationSimplified);
        //    if (cameraMovement > 0 && currentCameraRotationSimplified < maxDronViewRotation)
        //    {
        //        Quaternion rotation = gameObject.transform.rotation;
        //        rotation.x += cameraMovement * Time.deltaTime * cameraMovementSpeed;
        //        Debug.Log("Rotation " + rotation);
        //        currentCameraRotationSimplified += rotation.x + 10;
        //        dronView.transform.Rotate(new Vector3(rotation.x, 0, 0), rotation.x * 10, Space.Self);
        //    }
        //    else if (cameraMovement < 0 && currentCameraRotationSimplified > minDronViewRotation)
        //    {

        //        Quaternion rotation = gameObject.transform.rotation;
        //        rotation.x += cameraMovement * Time.deltaTime * cameraMovementSpeed;
        //        Debug.Log("Rotation " + rotation);
        //        currentCameraRotationSimplified -= rotation.x + 10;
        //        dronView.transform.Rotate(new Vector3(-rotation.x,0,0),rotation.x * 10,Space.Self);
        //    }
        //}

        //if (CheckIfGrounded() && inputDirection != Vector2.zero)
        //{
        //    playerOnGroundFeedback.SetActive(true);
        //}
        //else
        //{
        //    playerOnGroundFeedback.SetActive(false);
        //}
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
    private void MoveCamera()
    {

    }
    public void StartDron()
    {
        PlayerReferences.instance.SetDron(gameObject);
        PlayerStateController.instance.CameraToDron(gameObject);
        PlayerStateController.instance.StopMoving();
        canMove = true;
        GetComponent<Animator>().SetBool("flying", true);
        PlayerReferences.instance.GetHUD().SetActive(true);
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
    public bool IsGrounded()
    {
        return isGrounded;
    }
}


