using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronControllerAuto : MonoBehaviour
{
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
    private MovementBehaviour MB
        ;

    private bool isGrounded;
    private bool isMoving = true;

    void Start()
    {
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

        // Calculate direction to the waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);

        // Determine speed based on distance and angle
        float currentSpeed = normalSpeed;
        // Reduce speed near waypoint
        if (distanceToWaypoint < waypointTolerance * 5)
        {
            currentSpeed = reducedSpeed;
        }
        // Reduce speed before sharp turns
        float angle = Vector3.Angle(transform.forward, direction);
        if (angle > 45f)
        {
            currentSpeed = reducedSpeed;
        }

        // Move the drone
        MB.MoveDronAuto(direction, currentSpeed);

        // Rotate the drone smoothly towards the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

        // Check if the drone has reached the waypoint
        if (distanceToWaypoint <= waypointTolerance)
        {
            currentWaypointIndex++;

            // Stop moving if all waypoints are visited
            if (currentWaypointIndex >= waypoints.Count)
            {
                isMoving = false;
                Debug.Log("Drone has reached all waypoints.");
                this.gameObject.SetActive(false);
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
        Debug.Log("Drone started.");
    }
}