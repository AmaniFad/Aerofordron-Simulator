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
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private Vector3 targetPosition;
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
        Cursor.visible = false;
    }

    public void Interact()
    {
        previousPosition = transform.position;
        previousRotation = transform.localRotation;
        Cursor.visible = true;
        isPickable = false;

        player.GrabItem(this.gameObject);

        PlayerStateController.instance.StopMoving();

        Quaternion rotate = new Quaternion(0, 0, 0, 0);
        transform.rotation = rotate;

        //this.transform.localRotation = rotationOffset;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        GetComponent<Collider>().isTrigger = true;

        if (!isPickable)
        {
            isTaked.Invoke();
        }
        transform.rotation = rotate;
        transform.localRotation = targetRotation;
        transform.localPosition = targetPosition;
    }

    public void GetPlayerDropObj()
    {
        player.GetComponent<PlayerInteract>().DropObject();
    }
  
}
