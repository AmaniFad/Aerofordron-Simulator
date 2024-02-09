using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMando : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController player;
    private bool isPickable;
    public void Interact()
    {
        isPickable = false;
        //pC.GrabItem(this);
        //this.transform.localRotation = rotationOffset;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().isTrigger = true;
        if (!isPickable)
        {
            //isTaked.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPickable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
