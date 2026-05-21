using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private static PlayerController Instance;
    [SerializeField] private float runMultiplier;
    private Transform cameraTransform;
    private MovementBehaviour MB;
    private PlayerInteract playerInteract;
    private FMOD.Studio.EventInstance foosteps;
    private Coroutine isMoving;
    private bool wasCanMove;
    [SerializeField] private GameObject modelPlayer;
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

    private void FixedUpdate()
    {
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        modelPlayer.transform.localRotation = Quaternion.LookRotation(-cameraForward);


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
            
            Vector3 cameraRight = cameraTransform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 input = cameraForward * playerWasd.y + cameraRight * playerWasd.x;

            if (!wasCanMove)
            {
                modelPlayer.GetComponent<Animator>().SetTrigger("happy");
            }

            if (isMoving == null && input != Vector3.zero)
            {
                isMoving = StartCoroutine(_PlayFootstep());
            }
            if (PlayerInputController.Instance.IsRunning())
            {
                MB.RunRB(input, runMultiplier);
                modelPlayer.GetComponent<Animator>().SetFloat("Blend", 1f);
            }
            else
            {
                MB.MoveRB3D(input);
                modelPlayer.GetComponent<Animator>().SetFloat("Blend", 0.3f);
            }
        }
        else
        {
            if (wasCanMove)
            {
                modelPlayer.GetComponent<Animator>().SetTrigger("grab");
            }
        }
        if (PlayerInputController.Instance.GetPlayerInput() == Vector2.zero)
        {
            MB.StopMoving();
            modelPlayer.GetComponent<Animator>().SetFloat("Blend", 0f);
        }
        wasCanMove = PlayerStateController.instance.CanMove();
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
