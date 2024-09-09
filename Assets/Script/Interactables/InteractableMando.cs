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
    public Vector3 desiredRotation;
    private bool activateController;
    void Start()
    {
        activateController = false;
        rigidBody = GetComponent<Rigidbody>();
        controller = dron.GetComponent<DronController>();
        isPickable = true;
        player = PlayerReferences.instance.GetPlayer().GetComponent<PlayerInteract>();
    }

    private void Update()
    {
        if (activateController)
        {
            ActivateDron();
        }
        else
        {
            DeactivateDron();
        }
    }
    public void Interact()
    {
        //previousPosition = transform.position;
        //isPickable = false;
        //Quaternion rotate = new Quaternion(0,0,0,0);
        //transform.rotation = rotate;
        //player.GrabItem(this.gameObject);
        //controller.StartDron();
        ////this.transform.localRotation = rotationOffset;
        //rigidBody.useGravity = false;
        //rigidBody.isKinematic = true;
        //GetComponent<Collider>().isTrigger = true;
        //if (!isPickable)
        //{
        //    isTaked.Invoke();
        //}
        //transform.rotation = rotate;
    }


    public void StartDronFromController()
    {
        activateController = true;
        controller.StartDron();
    }

    private void ActivateDron()
    {
        if (controller != null)
        controller.StartDron();
    }

    private void DeactivateDron()
    {
        if (controller != null)
        controller.StopDron();
    }
    public void StopDronFromController()
    {
        activateController = false;
    }
    public void DropInteractable()
    {
        //controller.StopDron();
        //isPickable = true;
        //GetComponent<Collider>().isTrigger = false;
        //transform.SetParent(null);
        //transform.position = previousPosition;
        //rigidBody.useGravity = true;
        //rigidBody.isKinematic = false;
    }
}
