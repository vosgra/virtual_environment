using System.Collections;
using UnityEngine;

public class NPCMovement1 : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints; // Waypoints for the NPC to follow
    public float moveSpeed = 3f; // Speed at which the NPC moves
    public float waypointThreshold = 1f; // Distance threshold to consider a waypoint reached

    [Header("Idle Settings")]
    public float idleDuration = 30f; // Idle duration at each waypoint
    public float firstWaypointIdleDuration = 5f; // Idle duration at the first waypoint

    private int currentWaypointIndex = 0; // Current waypoint index
    private bool isIdle = false; // To check if the NPC is in idle state

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
    }

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        if (isIdle)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
        }

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold)
        {
            StartCoroutine(IdleAtWaypoint(currentWaypointIndex == 0 ? firstWaypointIdleDuration : idleDuration));
        }
    }

    private IEnumerator IdleAtWaypoint(float duration)
    {
        isIdle = true;

        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", true);
        }

        Debug.Log($"NPC is idling at waypoint for {duration} seconds.");
        yield return new WaitForSeconds(duration);

        isIdle = false;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
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
            for (int i = 0; i < waypoints.Length; i++)
            {
                int nextIndex = (i + 1) % waypoints.Length;
                Gizmos.DrawLine(waypoints[i].position, waypoints[nextIndex].position);
            }
        }
    }
}
