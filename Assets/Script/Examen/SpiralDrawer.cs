using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralDrawer : MonoBehaviour
{
    [Header("Spiral Manager")]
    [SerializeField] private int numPoints = 250;
    [SerializeField] private float startRadius = 1f; 
    [SerializeField] private float spaceBetweenTurns = 0.2f; 
    [SerializeField] private float angleMultiplier = 5f; 
    private LineRenderer lineRenderer;
    private Vector3 startPosition;

    [Header("GameObject")]
    [SerializeField] private GameObject lastPoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        startPosition = transform.position; 

        DrawSpiral();
    }

    void DrawSpiral()
    {
        lineRenderer.positionCount = numPoints;
        Vector3[] points = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * angleMultiplier * Mathf.PI / numPoints; 
            float radius = startRadius + (i * 0.01f); 
            float x = radius * Mathf.Cos(angle);
            float y = -i * spaceBetweenTurns; 
            float z = radius * Mathf.Sin(angle);

            points[i] = startPosition + new Vector3(x, y, z); 
        }

        lineRenderer.SetPositions(points);

        if (lastPoint != null)
        {
            lastPoint.transform.position = points[numPoints - 1];
        }
    }
}
