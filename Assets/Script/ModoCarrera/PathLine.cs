using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
    public Transform[] waypoints; // Array de puntos por donde pasará la línea
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (waypoints != null && waypoints.Length > 0)
        {
            lineRenderer.positionCount = waypoints.Length;
            for (int i = 0; i < waypoints.Length; i++)
            {
                lineRenderer.SetPosition(i, waypoints[i].position);
            }
        }
    }
}
