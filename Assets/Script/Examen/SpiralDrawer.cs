using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralDrawer : MonoBehaviour
{
    [Header("Spiral Manager")]
    [SerializeField] private int numPoints = 200; // Número de puntos en la espiral
    [SerializeField] private float startRadius = 1f; // Radio inicial
    [SerializeField] private float spaceBetweenTurns = 0.2f; // Espaciado vertical entre vueltas
    [SerializeField] private float angleMultiplier = 5f; // Controla la cantidad de giros
    private LineRenderer lineRenderer;
    private Vector3 startPosition; // Posición inicial de la espiral

    [Header("GameObject")]
    [SerializeField] private GameObject lastPoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        startPosition = transform.position; // Usar la posición del objeto en la escena

        DrawSpiral();
    }

    void DrawSpiral()
    {
        lineRenderer.positionCount = numPoints;
        Vector3[] points = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * angleMultiplier * Mathf.PI / numPoints; // Ángulo de rotación
            float radius = startRadius + (i * 0.01f); // Radio crece levemente
            float x = radius * Mathf.Cos(angle);
            float y = -i * spaceBetweenTurns; // Bajar la espiral
            float z = radius * Mathf.Sin(angle);

            points[i] = startPosition + new Vector3(x, y, z); // Ajusta la posición inicial
        }

        lineRenderer.SetPositions(points);

        if (lastPoint != null)
        {
            lastPoint.transform.position = points[numPoints - 1];
        }
    }
}
