using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DronInteraction : MonoBehaviour
{
    [SerializeField] Transform camilla;
    [SerializeField] private GameObject interactFeedback;
    private GameObject currentFeedback;
    private bool onlyThisFrame;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DropObject();
    }
    void Update()
    {
        if (Vector3.Distance(camilla.position, this.transform.position) < 5f)
        {
            if (currentFeedback == null)
            {
                currentFeedback = Instantiate(interactFeedback);
            }
            else
            {
                currentFeedback.SetActive(true);
            }
        }
        else
        {
            if (currentFeedback != null)
            {
                currentFeedback.SetActive(false);
            }
        }
    }
    private void FixedUpdate()
    {
        if(PlayerStateController.instance.CanMove() == false)
        {
            if (PlayerInputController.Instance.IsLoading())
            {
                Debug.Log("click l");
                TryToInteract();
                PlayerInputController.Instance.HasLoaded();
            }
        }
    }
    public void GrabItem(bool isLoaded)
    {
        this.gameObject.GetComponent<DronController>().SetIsLoaded(isLoaded);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = 12;
        rb.linearDamping = 1;
        rb.angularDamping = 1;
    }
    public void TryToInteract()
    {
        if (!onlyThisFrame)
        {

            float distance = Vector3.Distance(camilla.position, this.transform.position);
            if (this.gameObject.GetComponent<DronController>().GetIsLoaded())
            {
                Debug.Log("Drop");
                camilla.GetComponent<IInteractable>().DropInteractable();
                //sound.CallOneShot("event:/Grab");
            }
            else
            {
                // Perform the raycast
                if (distance < 5f)
                {
                    // Check if the hit object implements the IInteract interface
                    IInteractable interactableObject = camilla.gameObject.GetComponent<IInteractable>();
                    if (interactableObject != null)
                    {
                        //sound.CallOneShot("event:/Grab");
                        // Call the Interact method on the hit object
                        interactableObject.Interact();
                        Debug.Log("grab item");
                    }
                }
            }
            StartCoroutine(ChangeBoolInSeconds(0.2f));
        }
    }
    internal void DropObject()
    {
        this.gameObject.GetComponent<DronController>().SetIsLoaded(false);
        rb.interpolation = RigidbodyInterpolation.None;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.mass = 1;
        rb.linearDamping = 1.2f;
        rb.angularDamping = 0.3f;
    }
    private IEnumerator ChangeBoolInSeconds(float time)
    {
        onlyThisFrame = true;
        yield return new WaitForSeconds(time);
        onlyThisFrame = false;
    }
}
