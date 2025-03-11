using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints; // Array de puntos por donde pasará la línea
    private LineRenderer lineRenderer;

    public static PathLine instance;

    public Transform[] GetPoints() { return waypoints; }
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        lineRenderer = GetComponent<LineRenderer>();
        if (waypoints != null && waypoints.Length > 0)
        {
            lineRenderer.positionCount = waypoints.Length;
            for (int i = 0; i < waypoints.Length; i++)
            {
                lineRenderer.SetPosition(i, waypoints[i].position);
                //waypoints[i].gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void Restart()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
