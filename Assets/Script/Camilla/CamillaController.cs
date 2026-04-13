using UnityEngine;
using UnityEngine.Rendering;

public class CamillaController : MonoBehaviour , IInteractable
{
    [Header("References")]
    [SerializeField] private Transform drone;
    private Rigidbody rb;

    [Header("Spring Join values")]
    [SerializeField] private float cableLength;

    private SpringJoint springJoint;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Interact()
    {
        springJoint = gameObject.AddComponent<SpringJoint>();
        if (springJoint != null)
        {
            springJoint.connectedBody = drone.GetComponent<Rigidbody>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.anchor = Vector3.zero;
            springJoint.connectedAnchor = Vector3.zero;
            springJoint.spring = 80;
            springJoint.damper = 120f;
            springJoint.minDistance = 2.5f;
            springJoint.maxDistance = cableLength;
            springJoint.tolerance = 0.025f;
            springJoint.enablePreprocessing = true;
            springJoint.massScale = 1;
            springJoint.connectedMassScale = 1;

            drone.GetComponent<DronInteraction>().GrabItem(true);
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
    public void DropInteractable()
    {
        if (springJoint != null)
        {
            Destroy(gameObject.GetComponent<SpringJoint>());
            drone.GetComponent<DronInteraction>().DropObject();
        }
    }
}
