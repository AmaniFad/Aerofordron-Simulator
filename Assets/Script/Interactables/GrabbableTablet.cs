using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

public class GrabbableTablet : MonoBehaviour, IInteractable
{
    private PlayerInteract player;
    [SerializeField] private UnityEvent isTaked;
    private bool isPickable;
    private Vector3 previousPosition;
    private Rigidbody rigidBody;
    private Quaternion previousRotation;
   
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isPickable = true;
        player = PlayerReferences.instance.GetPlayer().GetComponent<PlayerInteract>();
    }
    public void DropInteractable()
    {
        isPickable = true;
        GetComponent<Collider>().isTrigger = false;

        transform.SetParent(null);

        PlayerStateController.instance.ResumeMoving();

        transform.position = previousPosition;
        transform.rotation = previousRotation;

        //rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
    }

    public void Interact()
    {
        previousPosition = transform.position;
        previousRotation = transform.localRotation;

        isPickable = false;

        Quaternion rotate = new Quaternion(0, 0, 0, 0);
        transform.rotation = rotate;

        player.GrabItem(this.gameObject);
        PlayerStateController.instance.StopMoving();

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

  
}
