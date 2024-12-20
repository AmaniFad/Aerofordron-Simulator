using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronGost : MonoBehaviour
{
    [Header("Path Points")]
    public Transform[] pathPoints; // Puntos del camino
    public float maxHeightOffset = 2f; // Altura máxima sobre el punto objetivo
    public float minHeightOffset = 1f; // Altura mínima sobre el punto objetivo
    public float maxDistanceFromPoint = 0.5f; // Distancia mínima para considerar que llegó al punto
    public float speed = 5f; // Velocidad base
    public float rotationSpeed = 5f; // Velocidad de rotación
    public float hoverForce = 9.8f; // Fuerza para mantener el hover

    [Header("Physics")]
    private Rigidbody rb;
    private int currentPointIndex = 0; // Índice del punto actual
    private bool isMoving = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Bloquear la rotación en los ejes X y Z
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Desactivar la gravedad en el eje Y
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        // Asegúrate de que el dron esté siguiendo el camino
        if (isMoving && pathPoints.Length > 0)
        {
            Transform targetPoint = pathPoints[currentPointIndex];
            Vector3 targetPosition = targetPoint.position + Vector3.up * Random.Range(minHeightOffset, maxHeightOffset);

            MoveTowardsPoint(targetPosition);
            RotateTowardsPoint(targetPosition);

            // Verifica si el dron ha llegado al punto objetivo
            if (Vector3.Distance(transform.position, targetPosition) <= maxDistanceFromPoint)
            {
                currentPointIndex++;
                if (currentPointIndex >= pathPoints.Length)
                {
                    isMoving = false;
                    gameObject.SetActive(false);
                }
            }
        }

        // Aplicar fuerza de hover para estabilizar la altura
        ApplyHoverForce();
    }

    private void MoveTowardsPoint(Vector3 targetPosition)
    {
        // Calculamos la dirección horizontal en X y Z
        Vector3 horizontalDirection = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z).normalized;

        // Diferencia en el eje Y (altura)
        float verticalDirection = targetPosition.y - transform.position.y;

        // Movimiento en X y Z (sin cambio en la altura)
        rb.velocity = new Vector3(horizontalDirection.x * speed, rb.velocity.y, horizontalDirection.z * speed);

        // Suavizamos el cambio en el eje Y (altura)
        if (Mathf.Abs(verticalDirection) > 0.05f)  // Ajusta este valor según la precisión necesaria
        {
            // Calculamos un valor de suavizado en Y usando Lerp
            float targetYVelocity = Mathf.Sign(verticalDirection) * speed;

            // Aplicamos un "damping" para suavizar el cambio de altura (más pequeño, más suave)
            float smoothYVelocity = Mathf.Lerp(rb.velocity.y, targetYVelocity, 0.05f);  // Reduce el 0.1f para un movimiento aún más suave

            rb.velocity = new Vector3(rb.velocity.x, smoothYVelocity, rb.velocity.z);
        }
        else
        {
            // Cuando estamos cerca de la altura deseada, mantenemos la velocidad en Y en 0
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    private void RotateTowardsPoint(Vector3 targetPosition)
    {
        // Dirección hacia el punto objetivo (sin la componente Y)
        Vector3 direction = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z).normalized;

        // Solo rota en el eje Y
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Asegúrate de usar `rb.MoveRotation` solo en el eje Y
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }
    private void ApplyHoverForce()
    {
        // Fuerza de hover para mantener al dron a una altura constante
        float hoverHeight = 1f; // Ajusta esto según la altura deseada
        float forceMagnitude = hoverForce * (transform.position.y - hoverHeight);

        rb.AddForce(Vector3.up * forceMagnitude, ForceMode.Force);
    }

}

