using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints
    public float speed = 5f;      // Speed of movement
    public bool loop = true;     // Should the object loop back to the first waypoint?
    public float rotationSpeed = 5f; // Speed of rotation
    public float rotationThreshold = 5f; // Degrees threshold to consider rotation complete

    private int currentWaypointIndex = 0; // Current waypoint index

    void Update()
    {
        if (waypoints.Length == 0) return; // Do nothing if there are no waypoints

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;

        // Rotate to face the target waypoint
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        directionToTarget.y = 0; // Keep rotation on the horizontal plane
        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget) * Quaternion.Euler(0, 170, 0); // Rotate Y-axis by 170 degrees
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if the rotation is within the threshold
        float angleDifference = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(directionToTarget) * Quaternion.Euler(0, 170, 0));
        if (angleDifference < rotationThreshold)
        {
            // Move the object towards the target waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the object has reached the waypoint
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;

                // Check if we need to loop or stop
                if (currentWaypointIndex >= waypoints.Length)
                {
                    if (loop)
                    {
                        currentWaypointIndex = 0; // Loop back to the first waypoint
                    }
                    else
                    {
                        enabled = false; // Stop the script if not looping
                    }
                }
            }
        }
    }

    // Draw the waypoints in the editor for visualization
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.3f);
                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }

        // Draw a line back to the start if looping
        if (loop && waypoints.Length > 1)
        {
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
        }
    }
}
