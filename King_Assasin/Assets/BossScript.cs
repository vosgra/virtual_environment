using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the cube points
    public float moveSpeed = 3f; // Speed at which the NPC moves
    public float waypointThreshold = 0.1f; // Distance threshold to consider the waypoint reached
    public float idleDuration = 1f; // Default time the NPC stays idle at each waypoint

    private int currentWaypointIndex = 0; // Index of the current waypoint
    private bool isIdle = false; // State to track if the NPC is idle
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on NPC!");
        }
    }

    void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return;
        }

        // Move the NPC only if it is not idle
        if (!isIdle)
        {
            MoveToWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        // Get the target waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Rotate the NPC to face the waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);

        // Move the NPC towards the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Set the walking animation
        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
        }

        // Check if the NPC is close enough to the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold)
        {
            StartCoroutine(IdleAtWaypoint());
        }
    }

    IEnumerator IdleAtWaypoint()
    {
        // Enter idle state
        isIdle = true;

        // Stop walking animation and start idle animation
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", true);
        }

        Debug.Log($"NPC is idle at waypoint {currentWaypointIndex}");

        // Determine idle duration based on waypoint index
        float currentIdleDuration = (currentWaypointIndex == 0) ? 4f : 0f;

        // Wait for the specified idle duration if applicable
        if (currentIdleDuration > 0)
        {
            yield return new WaitForSeconds(currentIdleDuration);
        }

        // Exit idle state
        isIdle = false;

        // Set the walking animation back
        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
        }

        Debug.Log($"NPC is walking to the next waypoint.");

        // Move to the next waypoint
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void OnDrawGizmos()
    {
        // Draw lines between waypoints in the editor
        if (waypoints != null && waypoints.Length > 1)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < waypoints.Length; i++)
            {
                int nextIndex = (i + 1) % waypoints.Length;
                Gizmos.DrawLine(waypoints[i].position, waypoints[nextIndex].position);
            }
        }
    }
}
