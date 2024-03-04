using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    private Transform interactionZone;
    private GameObject grabbeableObj;
    private Vector3 grabbeableObjOriginalScale;
    private PlaySounds sound;

    private void Start()
    {
        sound = GetComponent<PlaySounds>();
        grabbeableObj = null;
    }
    public void GrabItem(GameObject grabbeable)
    {
        interactionZone = InteractionZone.Instance.GetInteractionZone();

        Quaternion rotation = grabbeable.transform.rotation;

        grabbeable.transform.rotation = Quaternion.identity;
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
        Debug.Log("InteractStart");
        // Cast a ray from the position of this object forward
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        Debug.DrawRay(Camera.main.transform.position,ray.direction);
        RaycastHit hitInfo ; // Information about the object hit by the ray

        
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
                    Debug.Log("Interact");
                    // Call the Interact method on the hit object
                    interactableObject.Interact();
                }
            }
        }    
    }
}
