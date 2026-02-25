using UnityEngine;

public class CamillaController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform drone;              // Referencia al dron
    private Rigidbody rb;
    public SpringJoint springJoint;

    [Header("Ajustes de seguimiento")]
    public float cableLength = 2f;       // Distancia vertical bajo el dron

    [Header("Velocidad máxima")]
    public float maxVerticalSpeed = 3f;  // Máxima velocidad vertical de la camilla

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (springJoint != null)
        {
            springJoint.connectedBody = drone.GetComponent<Rigidbody>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.anchor = Vector3.zero;               // punto central de la camilla
            springJoint.connectedAnchor = Vector3.zero;
        }
    }
    [System.Obsolete]
    void FixedUpdate()
    {
        if (drone == null) return;

        // 1️⃣ Posición vertical deseada (solo Y)
        Vector3 targetPositionY = drone.position;
        targetPositionY.y -= cableLength;

        // 2️⃣ Limitar velocidad vertical
        Vector3 vel = rb.velocity;
        float verticalSpeed = targetPositionY.y - transform.position.y;
        vel.y = Mathf.Clamp(verticalSpeed / Time.fixedDeltaTime, -maxVerticalSpeed, maxVerticalSpeed);
        rb.velocity = vel;
    }
}
