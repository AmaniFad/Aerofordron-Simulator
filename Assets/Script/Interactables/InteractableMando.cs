using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableMando : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInteract player;
    [SerializeField] private UnityEvent isTaked;
    private bool isPickable;
    public void Interact()
    {
        isPickable = false;
        player.GrabItem(this);
       
        //this.transform.localRotation = rotationOffset;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().isTrigger = true;
        if (!isPickable)
        {
            isTaked.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPickable = true;
    }

    public void GetDropped(InteractableMando grabbeable)
    {
        if (grabbeable != null)
        {
            grabbeable.GetComponent<InteractableMando>().isPickable = true;
            grabbeable.transform.SetParent(null);

            grabbeable.GetComponent<Rigidbody>().useGravity = true;
            grabbeable.GetComponent<Rigidbody>().isKinematic = false;
            grabbeable.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
