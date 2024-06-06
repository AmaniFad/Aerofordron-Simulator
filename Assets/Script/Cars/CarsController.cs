using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsController : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private int startPoint;
    [SerializeField] private Transform[] points;
    private Vector3 direction;
    private int i;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 1.0f;
    private Quaternion targetRotation;

    [Header("Scripts")]
    private MovementBehaviour MB;

    void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
            if (points[i].GetComponent<PathPoint>().GetChangeDirection())
            {
                Vector3 direction = points[i].position - transform.position;
                if (direction != Vector3.zero)
                {
                    targetRotation = Quaternion.LookRotation(direction);
                }
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        direction = points[i].position - transform.position;
    }
}
