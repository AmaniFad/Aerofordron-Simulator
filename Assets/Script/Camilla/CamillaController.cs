using UnityEngine;

public class CamillaController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform drone;              // Referencia al dron
    public SpringJoint springJoint;

    [Header("Ajustes de seguimiento")]
    public float cableLength;       // Distancia vertical bajo el dron

    [Header("Velocidad máxima")]
    public float maxVerticalSpeed;  // Máxima velocidad vertical de la camilla

    void Start()
    {
        if (springJoint != null)
        {
            springJoint.connectedBody = drone.GetComponent<Rigidbody>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.anchor = Vector3.zero;               // punto central de la camilla
            springJoint.connectedAnchor = Vector3.zero;
        }
    }
}
