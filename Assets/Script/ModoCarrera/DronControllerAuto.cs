using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DronControllerAuto : MonoBehaviour
{
    #region variables
    [Header("Distances")]
    [SerializeField] private float maxHeight;
    [SerializeField] private float groundedRayDistance;
    [SerializeField] private float waypointTolerance = 0.5f;

    [Header("Speeds")]
    [SerializeField] private float normalSpeed;
    [SerializeField] private float reducedSpeed;

    [Header("Waypoints")]
    [SerializeField] private List<Transform> waypoints;
    public int currentWaypointIndex = 0;

    [Header("References")]
    private MovementBehaviour MB;

    [Header("Event")]
    [SerializeField] private UnityEvent _LastPointEvent;

    private bool isMoving = true;
    #endregion
    #region getters and setters
    public void SetNormalSpeed(float normalSpeed)
    {
        this.normalSpeed = normalSpeed;
        reducedSpeed = normalSpeed - 100;
    }
    public void SetCurrentWaypointIndex(int currentWaypointIndex)
    {
        this.currentWaypointIndex = currentWaypointIndex;
    }
    #endregion
    void Start()
    {
        waypoints = new List<Transform>();
        waypoints = PathLine.instance.GetPoints();

        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned. The drone will not move.");
            isMoving = false;
        }

        if (MB == null)
        {
            MB = GetComponent<MovementBehaviour>();
        }
    }

    private void FixedUpdate()
    {
        if (isMoving && waypoints != null && waypoints.Count > 0)
        {
            NavigateToWaypoint();
        }
    }

    private void NavigateToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // direccion
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);

        float currentSpeed = normalSpeed;
        //reducir la velocidad
        if (distanceToWaypoint < waypointTolerance * 5)
        {
            currentSpeed = reducedSpeed;
        }
        // reducir la velocidad antes de girar
        float angle = Vector3.Angle(transform.forward, direction);
        if (angle > 45f)
        {
            currentSpeed = reducedSpeed;
        }
        //para que baje esteticamente
        if(targetWaypoint.gameObject.GetComponent<PointDown>() != null)
        {
            MB.MoveDronAuto(Vector3.down, currentSpeed);
        }
        else
        {
            MB.MoveDronAuto(direction, currentSpeed);

            // retacion suave
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }

        //ultimo ounto
        if (distanceToWaypoint <= waypointTolerance)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                isMoving = false;
                Debug.Log("Drone has reached all waypoints.");;
                _LastPointEvent.Invoke();
            }
        }
    }

    private bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundedRayDistance);
    }

    public void SetWaypoints(List<Transform> newWaypoints)
    {
        waypoints = newWaypoints;
        currentWaypointIndex = 0;
        isMoving = true;
    }

    public void StopDrone()
    {
        isMoving = false;
        MB.StopMovingOnY();
        Debug.Log("Drone stopped.");
    }

    public void StartDrone()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints set. Please set waypoints before starting the drone.");
            return;
        }

        isMoving = true;
    }
}