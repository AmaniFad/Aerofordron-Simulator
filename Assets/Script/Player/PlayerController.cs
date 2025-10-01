using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerController : MonoBehaviour
{
    private static PlayerController Instance;
    [SerializeField] private float runMultiplier;
    private Transform cameraTransform;
    private MovementBehaviour MB;
    private PlayerInteract playerInteract;
    private FMOD.Studio.EventInstance foosteps;
    private Coroutine isMoving;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor interactor;
    [SerializeField] private DynamicMoveProvider movement;


    private void OnEnable()
    {

    }
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
        isMoving = null;
        MB = GetComponent<MovementBehaviour>();
        cameraTransform = Camera.main.transform;
        Cursor.visible = false;
        playerInteract = GetComponent<PlayerInteract>();
    }
    private void Update()
    {
        if (PlayerStateController.instance.CanMove())
        {
            movement.moveSpeed = 1;
        }
        else
        {
            movement.moveSpeed = 0;
        }
    }
    private void FixedUpdate()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        if (PlayerInputController.Instance.IsInteracting())
        {
            playerInteract.TryToInteract();
            PlayerInputController.Instance.HasInteracted();
        }
        if (PlayerStateController.instance.CanMove())
        {
            Vector2 playerWasd = PlayerInputController.Instance.GetPlayerInput();

            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;

            cameraForward.Normalize();
            cameraRight.Normalize();
            Vector3 input = cameraForward * playerWasd.y + cameraRight * playerWasd.x;
            if (input.x > -0.1f && input.x < 0.1f)
            {
                input.x = 0;
            }
            if (input.z > -0.1f && input.z < 0.1f)
            {
                input.z = 0;
            }
            if (isMoving == null && input != Vector3.zero)
            {
                isMoving = StartCoroutine(_PlayFootstep());
            }
            if (PlayerInputController.Instance.IsRunning())
            {
                MB.RunRB(input, runMultiplier);
            }
            else
            {
                MB.MoveRB3D(input);
            }
        }
    }


    private IEnumerator _PlayFootstep()
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
        yield return new WaitForSeconds(0.4f);
        isMoving = null;
    }



}
