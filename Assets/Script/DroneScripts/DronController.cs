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
    #region variables
    [Header("Distances")]
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private float groundedRayDistance;
    private bool isDronPlatformOn;
    private bool canMove;
    private MovementBehaviour mMovementBehaviour;
    private bool isPlaying;
    private float maxUpTilt = 1000;
    private float maxDownTilt = -700;
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
    private Rigidbody rb;
    float currentZTiltMultiplier = 1;

    [SerializeField] private float cameraMovementSpeed;
    private bool isGrounded;
    private float lastTiltZ;

    [Header("SpawnPoints")]
    [SerializeField] private Transform waypoints;

    private bool remoteDron;
    private bool aux;

    public float _attiMode;
    //POR IMPLEMENTAR
    //[SerializeField] private GameObject playerOnGroundFeedback;
    #endregion
    void Start()
    {

        rb = GetComponent<Rigidbody>(); 
        currentCameraTilt = 0;
        //currentCameraRotationSimplified = 0;
        eventEmitter = GetComponent<StudioEventEmitter>();
        isPlaying = false;
        mMovementBehaviour = GetComponent<MovementBehaviour>();
        //tiltAngle = 25;
        rotationSpeed = 150;
        PlayerReferences.instance.SetDron(this.gameObject);
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundedRayDistance);
    }
    private void Update()
    {
        if (eventEmitter)
        {
            eventEmitter.SetParameter("Speed", Mathf.Abs((rb.linearVelocity.x + rb.linearVelocity.y) / 2 / 60* 10));
            eventEmitter.SetParameter("Volume", Mathf.Abs((rb.linearVelocity.x + rb.linearVelocity.y) / 2 / 60 * 10));
        }
        if (DronInputController.Instance.GetCameraMovement() > 0)
        {
            MoveCameraDown();
        }
        if (DronInputController.Instance.GetCameraMovement() < 0)
        {
            MoveCameraUp();
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
            if (!remoteDron)
            {
                TryToMoveDron();
            }
            
            if (!eventEmitter.IsPlaying())
            {
                eventEmitter.Play();
            }
            returnToSpawn();
        }
        else
        {
            StopPlayDroneSound();
        }
    }

    private void LateUpdate()
    {

    }

    [System.Obsolete]
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

        _attiMode = DronInputController.Instance.GetModeAtti();

        //Debug.Log(CheckIfGrounded());
        if (!CheckIfGrounded())
        {
            Vector3 direction = transform.right * inputDirection.x + transform.forward * inputDirection.y;

            if (_attiMode == 1)
            {
                if (inputDirection.magnitude > 0.01f)
                {
                    mMovementBehaviour.Move(new Vector3(direction.x, 0, direction.z));
                    SendDronRotation(inputDirection);
                }
                else
                {
                    // Sueltas los sticks → que decaiga lentamente la velocidad
                    mMovementBehaviour.nonInputInputls(new Vector3(0, rb.velocity.y, 0), 0.05f);
                }
                if (GetComponent<WindObject>() != null)
                {
                    //mMovementBehaviour.MoveWithoutSpeed(WindControlller.Instance.GetWindForce());
                    GetComponent<WindObject>().SetWindDampen(1f);
                }
            }
            else // GPS Mode
            {
                GetComponent<WindObject>().SetWindDampen(0f);

                if (inputDirection.magnitude > 0.01f)
                {
                    mMovementBehaviour.Move(new Vector3(direction.x, 0, direction.z));
                    SendDronRotation(inputDirection);
                }
                else
                {
                    mMovementBehaviour.nonInputInputls(Vector3.zero,5f);
                }
            }
            SendDronRotation(inputDirection);
        }
        float cameraMovement = DronInputController.Instance.GetCameraMovement();
        /*if (cameraMovement != 0)
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
        }*/
    }
    // consume mas en modo atti
    private void OnDestroy()
    {
        StopPlayDroneSound();
    }
    private void SendDronRotation(Vector2 inputDirection)
    {
        //Necesario sino vuelve a 0 la rotation para los lados el momento que dejes de pulsar
        float currentYRotation = transform.rotation.eulerAngles.y;
        if (lastTiltZ != -inputDirection.x * tiltAngle && lastTiltZ != 0)
        {
            currentZTiltMultiplier = 2;
        }
        //Esto es para que tire un poco hacia el lado que se esta moviendo
        float tiltAroundZ =  -inputDirection.x * tiltAngle * currentZTiltMultiplier;

        float tiltAroundX = +inputDirection.y * tiltAngle;
        currentZTiltMultiplier = Mathf.Lerp(currentZTiltMultiplier, 1, 0.15f) ;
        if (tiltAroundZ != 0)
        lastTiltZ = -inputDirection.x * tiltAngle;

        print("Tilt: " + tiltAroundZ + " Direction " + -inputDirection.x);

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
            float viewRotation = 1 * Time.deltaTime * cameraMovementSpeed;
            currentCameraTilt += 10f; 
            dronView.transform.Rotate(viewRotation, 0, 0, Space.Self);
        }
    }

    private void MoveCameraDown()
    {
        if (currentCameraTilt > maxDownTilt)
        {
            float viewRotation = -1 * Time.deltaTime * cameraMovementSpeed;
            currentCameraTilt -= 10f; 
            dronView.transform.Rotate(viewRotation, 0, 0, Space.Self);
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
        PlayerReferences.instance.GetHUD().SetActive(false);
    }

    public void StopDron()
    {
        PlayerReferences.instance.SetDron(null);
        PlayerStateController.instance.CameraToDron(null);
        PlayerStateController.instance.ResumeMoving();
        canMove = false;
        GetComponent<Animator>().SetBool("flying", false);
        PlayerReferences.instance.GetHUD().SetActive(true);
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
        if (!PlayerStateController.instance.CanMove())
        canMove = true;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    private void returnToSpawn()
    {
        if (DronInputController.Instance.GetRemoteDron())
        {
            remoteDron = true;
            Debug.Log("oressed" + remoteDron);
        }
        if(remoteDron)
        {
            float speed = 750f;
            Transform targetWaypoint = waypoints;

            
            Vector3 direction = (targetWaypoint.position - transform.position);
            direction.y = 0f;
            direction.Normalize();
            
            float distanceY = Mathf.Abs(transform.position.y - targetWaypoint.position.y);
            float distanceAll = Vector3.Distance(transform.position, targetWaypoint.position);

            // Distancia horizontal
            float distanceXZ = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z),
                new Vector2(targetWaypoint.position.x, targetWaypoint.position.z)
            );
            if(distanceXZ < 20f)
            {
                speed = 50;
            }
            if (distanceXZ <= 0.3f || aux)
            {
                // Reducir velocidad gradualmente al descender
                speed = 60;
                mMovementBehaviour.MoveDronAuto(Vector3.down, speed); 

                // Estabilizar rotaci�n horizontal
                Quaternion stableRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, stableRotation, Time.deltaTime * 2f);

                aux = true;
            }
            else
            {
                // Avanzar horizontalmente
                mMovementBehaviour.MoveDronAuto(direction, speed);

                // Rotaci�n suave mirando hacia adelante
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            }

            if (distanceXZ <= 0.5f && distanceY <= 0.2f)
            {
                mMovementBehaviour.StopMovingOnY();
                remoteDron = false;
                aux = false;
            }
        }
    }
}
