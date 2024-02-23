using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class InteractableMando : MonoBehaviour, IInteractable
{
    private PlayerInteract player;
    [SerializeField] private UnityEvent isTaked;
    [SerializeField] private GameObject dron;
    DronController controller;
    private bool isPickable;
    private Vector3 previousPosition;

    void Start()
    {
        controller = dron.GetComponent<DronController>();
        isPickable = true;
        player = PlayerReferences.instance.GetPlayer().GetComponent<PlayerInteract>();
    }

    public void Interact()
    {
        previousPosition = transform.position;
        isPickable = false;
        player.GrabItem(this);
        controller.StartDron();
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


    public void GetDropped(InteractableMando grabbeable)
    {
        if (grabbeable != null)
        {
            controller.StopDron();
            grabbeable.GetComponent<InteractableMando>().isPickable = true;
            grabbeable.transform.SetParent(null);
            transform.position = previousPosition;
            grabbeable.GetComponent<Rigidbody>().useGravity = true;
            grabbeable.GetComponent<Rigidbody>().isKinematic = false;
            grabbeable.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
