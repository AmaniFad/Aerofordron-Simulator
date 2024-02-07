using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float raycastDistance = 10f; // Distance to cast the ray
    public LayerMask layerMask; // Layer mask to filter what objects the ray interacts with

    void Update()
    {
        if (PlayerInputController.Instance.IsInteracting())
        {
            TryToInteract();
        }
    }

    public void TryToInteract()
    {
        // Cast a ray from the position of this object forward
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hitInfo; // Information about the object hit by the ray

        // Perform the raycast
        if (Physics.Raycast(ray, out hitInfo, raycastDistance, layerMask))
        {

            // Check if the hit object implements the IInteract interface
            IInteractable interactableObject = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                // Call the Interact method on the hit object
                interactableObject.Interact();
            }
        }
    }

}
