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
    private Rigidbody rigidBody;
    private Collider dronPartCollider;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        controller = dron.GetComponent<DronController>();
        isPickable = true;
        player = PlayerReferences.instance.GetPlayer().GetComponent<PlayerInteract>();
    }

    public void Interact()
    {
        previousPosition = transform.position;
        isPickable = false;
        player.GrabItem(this.gameObject);
        controller.StartDron();
        //this.transform.localRotation = rotationOffset;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        dronPartCollider.isTrigger = true;
        if (!isPickable)
        {
            isTaked.Invoke();
        }

    }

    // Start is called before the first frame update


    public void DropInteractable()
    {
        controller.StopDron();
        isPickable = true;
        transform.SetParent(null);
        transform.position = previousPosition;
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        dronPartCollider.isTrigger = false;
    }
}
