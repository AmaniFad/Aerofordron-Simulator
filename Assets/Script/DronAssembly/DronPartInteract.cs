using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DronPartInteract : MonoBehaviour, IInteractable
{
    private PlayerInteract player;
    [SerializeField] private UnityEvent isTaken;
    private bool isPickable;
    private Transform startParent;
    private Rigidbody rigidBody;
    private Collider dronPartCollider;
    [SerializeField] private DronAssemblyController assemblyController;
    private bool isPut;
    void Start()
    {
        startParent = transform.parent;
        rigidBody = GetComponent<Rigidbody>();
        isPickable = true;
        player = PlayerReferences.instance.GetPlayer().GetComponent<PlayerInteract>();
        dronPartCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        //Mientras este pickeado el item comprueba si esta cerca
        if (!isPickable)
        {
            isPut = assemblyController.CheckIfClose(this.gameObject);
            if (isPut)
            {
                DropInteractable();
            }
        }
    }
    public void Interact()
    {
        player.GrabItem(this.gameObject);

        if (isPickable)
        {
            isTaken.Invoke();
            isPickable = false;
            rigidBody.useGravity = false;
            dronPartCollider.isTrigger = true;
        }

    }

    public void DropInteractable()
    {
        //Para diferenciar si lo has soltado o lo has puesto donde debias
        if (isPut)
        {
            dronPartCollider.isTrigger = true;
            rigidBody.useGravity = false;
        }
        else
        {
            dronPartCollider.isTrigger = false;
            rigidBody.useGravity = true;
        }
        isPickable = true;
        transform.SetParent(startParent);

    }



}