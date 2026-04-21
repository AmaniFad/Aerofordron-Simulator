using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>(); // Array de puntos por donde pasará la línea
    private LineRenderer lineRenderer;

    public static PathLine instance;

    //public Transform[] GetPoints() { return waypoints; }
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        lineRenderer = GetComponent<LineRenderer>();
        foreach(Transform child in transform)
        {
            waypoints.Add(child);
        }
        if (waypoints != null && waypoints.Count > 0)
        {
            lineRenderer.positionCount = waypoints.Count;
            for (int i = 0; i < waypoints.Count; i++)
            {
                lineRenderer.SetPosition(i, waypoints[i].position);
                //waypoints[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void Restart()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypoints[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
