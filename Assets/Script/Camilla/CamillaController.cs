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
            springJoint.minDistance = 10f;
            springJoint.maxDistance = cableLength;

            // --- CAMBIO CLAVE AQUÍ ---
            // Al poner massScale alto, la camilla siente mucho la fuerza.
            // Al poner connectedMassScale muy bajo (cercano a 0), 
            // el dron ignora casi por completo las fuerzas que le llegan de la camilla.
            springJoint.massScale = 1f;
            springJoint.connectedMassScale = 0.001f;

            // Evita que las colisiones entre dron y camilla vuelvan loco al sistema
            springJoint.enableCollision = false;
            springJoint.enablePreprocessing = true;

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
