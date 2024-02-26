using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DronPartInteract : MonoBehaviour, IInteractable
{
    private PlayerInteract player;
    [SerializeField] private UnityEvent isTaken;
    private bool isPickable;
    private Rigidbody rigidBody;
    private Collider dronPartCollider;
    [SerializeField] private DronAssemblyController assemblyController;
    private bool isPut;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isPickable = true;
        player = PlayerReferences.instance.GetPlayer().GetComponent<PlayerInteract>();
        dronPartCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!isPickable)
        {
            isPut = assemblyController.CheckIfClose(this.gameObject);
        }
    }
    public void Interact()
    {
        player.GrabItem(this.gameObject);

        if (isPickable)
        {
            isTaken.Invoke();
        }
        isPickable = false;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        dronPartCollider.isTrigger = true;
    }



    public void DropInteractable()
    {
        if (isPut)
        {
            dronPartCollider.isTrigger = true;
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
        }
        else
        {
            dronPartCollider.isTrigger = false;
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
        }
        isPickable = true;
        transform.SetParent(null);

    }

}
