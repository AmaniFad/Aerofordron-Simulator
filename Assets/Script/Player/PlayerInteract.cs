using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform InteractionZone;

    private InteractableMando grabbeableObj;
    private Vector3 grabbeableObjOriginalScale;

    private void Start()
    {
        grabbeableObj = null;
    }
    public void GrabItem(InteractableMando grabbeable)
    {
        grabbeableObjOriginalScale = grabbeable.transform.localScale;
        Quaternion rotation = grabbeable.transform.rotation;

        grabbeable.transform.rotation = Quaternion.identity;
        grabbeable.transform.SetParent(InteractionZone, true);

        // grabbeable.transform.rotation = rotation;
        grabbeable.transform.position = InteractionZone.position;

        grabbeableObj = grabbeable;
    }
    internal void DropObject()
    {
        grabbeableObj.transform.localScale = grabbeableObjOriginalScale;
        grabbeableObj = null;

    }
    public void TryToInteract()
    {
        // Cast a ray from the position of this object forward
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        Debug.DrawRay(Camera.main.transform.position,ray.direction);
        RaycastHit hitInfo ; // Information about the object hit by the ray


        if (grabbeableObj != null)
        {
            grabbeableObj.GetDropped(grabbeableObj);
            DropObject();
        }
        else
        {
            // Perform the raycast
            if (Physics.Raycast(ray, out hitInfo, raycastDistance, layerMask))
            {
                // Check if the hit object implements the IInteract interface
                IInteractable interactableObject = hitInfo.collider.gameObject.GetComponent<IInteractable>();
                Debug.Log(hitInfo.collider.gameObject);

                if (interactableObject != null)
                {
                    // Call the Interact method on the hit object
                    interactableObject.Interact();
                }
            }
        }    
    }
}
