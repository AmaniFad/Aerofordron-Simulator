using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronControllerAuto : MonoBehaviour
{
    [Header("Distances")]
    [SerializeField] private float maxHeight;
    [SerializeField] private float groundedRayDistance;
    [SerializeField] private float waypointTolerance = 0.5f;

    [Header("Waypoints")]
    [SerializeField] private List<Transform> waypoints;
    private int currentWaypointIndex = 0;

    [Header("References")]
    [SerializeField] private MovementBehaviour mMovementBehaviour;

    private bool isGrounded;
    private bool isMoving = true;

    void Start()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned. The drone will not move.");
            isMoving = false;
        }

        if (mMovementBehaviour == null)
        {
            mMovementBehaviour = GetComponent<MovementBehaviour>();
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

        // Ensure the drone stays below the max height
        if (transform.position.y >= maxHeight && direction.y > 0)
        {
            direction.y = 0;
        }

        // Move the drone
        mMovementBehaviour.Move(direction);

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
        mMovementBehaviour.StopMovingOnY();
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