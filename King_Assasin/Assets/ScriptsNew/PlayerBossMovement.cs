using System.Collections;
using UnityEngine;

public class PlayerBossMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints; // Waypoints for the NPC to follow
    public float moveSpeed = 3f; // Speed at which the NPC moves
    public float waypointThreshold = 1f; // Distance threshold to consider a waypoint reached

    [Header("Idle Settings")]
    public float idleDurationAtLastWaypoint = 5f; // Idle duration at the last waypoint

    [Header("GameObject Activation")]
    public float enableDuration = 30f; // Time in seconds the GameObject stays enabled

    private int currentWaypointIndex = 0; // Current waypoint index
    private bool isIdle = false; // Whether the NPC is idle
    private Animator animator; // Animator component reference

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on NPC!");
        }

        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned. NPC will not move.");
        }

        // Disable the GameObject after the specified duration
        if (enableDuration > 0)
        {
            StartCoroutine(DisableAfterDuration());
        }
    }

    void Update()
    {
        if (waypoints.Length == 0 || isIdle)
            return;

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Move towards the current waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Update animator states
        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
        }

        // Check if the NPC has reached the current waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold)
        {
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                // Reached the last waypoint, stay idle
                StartCoroutine(IdleAtLastWaypoint());
            }
            else
            {
                // Move to the next waypoint
                currentWaypointIndex++;
            }
        }
    }

    private IEnumerator IdleAtLastWaypoint()
    {
        isIdle = true;

        // Update animator states
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", true);
        }

        Debug.Log($"NPC is idling at the last waypoint for {idleDurationAtLastWaypoint} seconds.");
        yield return new WaitForSeconds(idleDurationAtLastWaypoint);

        // Optionally, you can disable the NPC or reset its position here
        Debug.Log("NPC finished idling at the last waypoint.");
    }

    private IEnumerator DisableAfterDuration()
    {
        yield return new WaitForSeconds(enableDuration);
        Debug.Log($"Disabling NPC after {enableDuration} seconds.");
        gameObject.SetActive(false); // Disable the GameObject
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawWaypointPath(waypoints);
    }

    private void DrawWaypointPath(Transform[] waypoints)
    {
        if (waypoints != null && waypoints.Length > 1)
        {
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}