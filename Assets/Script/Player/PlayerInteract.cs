using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject interactFeedback;
    private GameObject currentFeedback;
    private Transform interactionZone;
    private GameObject grabbeableObj;
    private Vector3 grabbeableObjOriginalScale;
    private PlaySounds sound;
    private bool onlyThisFrame;

    private void Start()
    {
        sound = GetComponent<PlaySounds>();
        grabbeableObj = null;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Physics.Raycast(ray, raycastDistance, layerMask) && grabbeableObj == null)
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
    public void GrabItem(GameObject grabbeable)
    {
        interactionZone = InteractionZone.Instance.GetInteractionZone();

        Quaternion rotation = grabbeable.transform.rotation;

        //grabbeable.transform.rotation = Quaternion.identity;
        grabbeable.transform.SetParent(interactionZone, true);


        // grabbeable.transform.rotation = rotation;
        grabbeable.transform.position = interactionZone.position;

        grabbeableObj = grabbeable;
    }
    internal void DropObject()
    {
        grabbeableObj = null;

    }
    public void TryToInteract()
    {
        if (!onlyThisFrame)
        {


            Debug.Log("TryToInteract");
            // Cast a ray from the position of this object forward
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hitInfo; // Information about the object hit by the ray


            if (grabbeableObj != null)
            {
                grabbeableObj.GetComponent<IInteractable>().DropInteractable();
                DropObject();
                sound.CallOneShot("event:/Grab");
            }
            else
            {
                // Perform the raycast
                if (Physics.Raycast(ray, out hitInfo, raycastDistance, layerMask))
                {
                    // Check if the hit object implements the IInteract interface
                    IInteractable interactableObject = hitInfo.collider.gameObject.GetComponent<IInteractable>();

                    if (interactableObject != null)
                    {
                        sound.CallOneShot("event:/Grab");
                        // Call the Interact method on the hit object
                        interactableObject.Interact();
                    }
                }
            }
            StartCoroutine(ChangeBoolInSeconds(0.2f));
        }
    }

    private IEnumerator ChangeBoolInSeconds(float time)
    {
        onlyThisFrame = true;
        yield return new WaitForSeconds(time);
        onlyThisFrame = false;
    }
}
