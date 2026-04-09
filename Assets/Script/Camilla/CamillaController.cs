using UnityEngine;
using UnityEngine.Rendering;

public class CamillaController : MonoBehaviour , IInteractable
{
    [Header("References")]
    [SerializeField] private Transform drone;

    [Header("Spring Join values")]
    [SerializeField] private float cableLength;
    [SerializeField] private float maxVerticalSpeed;

    private SpringJoint springJoint;
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
            springJoint.damper = 50;
            springJoint.minDistance = 0;
            springJoint.maxDistance = 2.5f;
            springJoint.tolerance = 0.025f;
            springJoint.enablePreprocessing = true;
            springJoint.massScale = 1;
            springJoint.connectedMassScale = 1;

            drone.GetComponent<DronInteraction>().GrabItem(true);
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
