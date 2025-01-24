using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWaypoints : MonoBehaviour
{
    [Header("Waypoints Settings")]
    public Transform[] waypoints; // List of waypoints to move through
    public float speed = 5f; // Speed of movement
    public float waypointThreshold = 0.1f; // Distance threshold to consider reaching a waypoint

    private int currentWaypointIndex = 0; // Tracks the current waypoint
    private bool isMoving = false; // Tracks if the object is moving

    void OnEnable()
    {
        if (waypoints.Length > 0)
        {
            isMoving = true;
            currentWaypointIndex = 0;
            StartCoroutine(MoveToNextWaypoint());
        }
        else
        {
            Debug.LogWarning("No waypoints assigned to the object.");
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (isMoving && currentWaypointIndex < waypoints.Length)
        {
            // Move towards the current waypoint
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            while (Vector3.Distance(transform.position, targetWaypoint.position) > waypointThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
                yield return null;
            }

            // Wait for a moment at the waypoint (optional, can add delay here if needed)
            yield return new WaitForSeconds(0.1f);

            // Move to the next waypoint
            currentWaypointIndex++;
        }

        // Stop moving after reaching the last waypoint
        if (currentWaypointIndex >= waypoints.Length)
        {
            isMoving = false;
            Debug.Log("Reached the final waypoint.");
        }
    }
}
