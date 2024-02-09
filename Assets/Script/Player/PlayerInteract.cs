using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    void Start()
    {
        
    }


    void Update()
    {

    }

    public void TryToInteract()
    {

        // Cast a ray from the position of this object forward
        Ray ray = new Ray(transform.position, Camera.main.transform.forward);
        Debug.DrawRay(transform.position, transform.forward,Color.red);
        RaycastHit hitInfo; // Information about the object hit by the ray

        // Perform the raycast
        if (Physics.Raycast(ray, out hitInfo, raycastDistance, layerMask))
        {

            // Check if the hit object implements the IInteract interface
            IInteractable interactableObject = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                Debug.Log("Interact");
                // Call the Interact method on the hit object
                interactableObject.Interact();
            }
        }
    }

}
