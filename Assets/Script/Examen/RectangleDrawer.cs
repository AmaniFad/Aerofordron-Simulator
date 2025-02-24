using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleDrawer : MonoBehaviour
{
    public GameObject[] objectsToArrange; // Asigna los objetos en el inspector
    public Vector2 rectangleSize = new Vector2(5, 3); // Tamaño del rectángulo (ancho x alto)
    private LineRenderer lineRenderer; // Asigna un LineRenderer en el inspector

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ArrangeObjectsInRectangle();
        DrawRectangle();
    }

    void ArrangeObjectsInRectangle()
    {
        if (objectsToArrange.Length < 4) return; // Se necesitan al menos 4 objetos

        Vector3 startPosition = objectsToArrange[0].transform.position;
        Vector3[] corners = new Vector3[4]
        {
            startPosition,
            startPosition + new Vector3(rectangleSize.x, 0, 0),
            startPosition + new Vector3(rectangleSize.x, 0, -rectangleSize.y),
            startPosition + new Vector3(0, 0, -rectangleSize.y)
        };

        for (int i = 0; i < 4; i++)
        {
            if (i < objectsToArrange.Length)
            {
                objectsToArrange[i].transform.position = corners[i];
            }
        }
    }

    void DrawRectangle()
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = 5; // Cuatro esquinas más la de cierre
        lineRenderer.loop = true;

        Vector3 startPosition = objectsToArrange[0].transform.position;
        Vector3[] points = new Vector3[5]
        {
            startPosition,
            startPosition + new Vector3(rectangleSize.x, 0, 0),
            startPosition + new Vector3(rectangleSize.x, 0, -rectangleSize.y),
            startPosition + new Vector3(0, 0, -rectangleSize.y),
            startPosition // Cierre del rectángulo
        };

        lineRenderer.SetPositions(points);
    }
}

