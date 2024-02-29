using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runMultiplier;
    private Transform cameraTransform;
    private MovementBehaviour MB;
    private PlayerInteract playerInteract;
    private FMOD.Studio.EventInstance foosteps;
    private Coroutine isMoving;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        isMoving = null;
        MB = GetComponent<MovementBehaviour>();
        cameraTransform = Camera.main.transform;
        Cursor.visible = false;
        playerInteract = GetComponent<PlayerInteract>();
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
