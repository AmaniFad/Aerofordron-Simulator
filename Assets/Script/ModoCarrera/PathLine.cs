using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new List<Transform>(); // Array de puntos por donde pasará la línea
    private LineRenderer lineRenderer;

    public static PathLine instance;

    public List<Transform> GetPoints() { return waypoints; }
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        lineRenderer = GetComponent<LineRenderer>();
        if(waypoints.Count == 0)
        {
            foreach (Transform child in transform)
            {
                waypoints.Add(child);
            }
        }
        if (waypoints != null && waypoints.Count > 0)
        {
            /*lineRenderer.positionCount = waypoints.Count;
            for (int i = 0; i < waypoints.Count; i++)
            {
                lineRenderer.SetPosition(i, waypoints[i].position);
                //waypoints[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            }*/
            DrawSmoothLine();
        }
    }
    void DrawSmoothLine()
    {
        List<Vector3> points = new List<Vector3>();
        List<Vector3> wp = new List<Vector3>();

        wp.Add(waypoints[0].position);

        foreach (var w in waypoints)
            wp.Add(w.position);

        wp.Add(waypoints[waypoints.Count - 1].position);
        for (int i = 0; i < wp.Count - 3; i++)
        {
            for (float t = 0; t <= 1; t += 0.1f)
            {
                points.Add(CatmullRom(
                    wp[i],
                    wp[i + 1],
                    wp[i + 2],
                    wp[i + 3],
                    t
                ));
            }
        }
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 0.5f * (
            (2 * p1) +
            (-p0 + p2) * t +
            (2 * p0 - 5 * p1 + 4 * p2 - p3) * t * t +
            (-p0 + 3 * p1 - 3 * p2 + p3) * t * t * t
        );
    }

    public void Restart()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypoints[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
