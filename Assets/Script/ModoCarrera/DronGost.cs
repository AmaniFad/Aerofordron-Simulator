using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronGost : MonoBehaviour
{
    private MovementBehaviour MB;
    public int startPoint;        // Punto inicial en el path
    public Transform[] points;    // Puntos del path

    private int i; // Índice actual del punto del path
    private Vector3 direction;

    void Start()
    {
        points = PathLine.instance.GetPoints();
        MB = GetComponent<MovementBehaviour>();
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
        }
        direction = points[i].position - transform.position;
        MB.MoveDronGost(direction);
    }
}

