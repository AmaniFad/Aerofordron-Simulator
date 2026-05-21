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
    private Quaternion previousRotation;

    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private Vector3 targetPosition;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        controller = dron.GetComponent<DronController>();
        isPickable = true;
        player = PlayerReferences.instance.GetPlayer().GetComponent<PlayerInteract>();
        previousPosition = transform.position;
        previousRotation = transform.localRotation;
    }

    public void Interact()
    {
        isPickable = false;

        player.GrabItem(this.gameObject);

        Quaternion rotate = new Quaternion(0,0,0,0);
        transform.rotation = rotate;
        transform.localRotation = targetRotation;
        transform.localPosition = targetPosition;

        controller.StartDron();
        //this.transform.localRotation = rotationOffset;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        if (!isPickable)
        {
            isTaked.Invoke();
        }
        transform.rotation = rotate;
    }

    public void DropInteractable()
    {
        controller.StopDron();
        isPickable = true;
        GetComponent<Collider>().isTrigger = false;
        transform.SetParent(null);
        transform.position = previousPosition;
        transform.rotation = previousRotation;
        //rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
    }
}
