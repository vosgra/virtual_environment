using System.Collections;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints; // Default waypoints for the NPC
    public Transform[] alternateWaypoints; // Alternate waypoints for the NPC
    public float moveSpeed = 3f; // Speed at which the NPC moves
    public float waypointThreshold = 1f; // Distance threshold to consider a waypoint reached

    [Header("Idle Settings")]
    public float idleDuration = 30f; // Idle duration at each waypoint (main path)
    public float firstWaypointIdleDuration = 5f; // Idle duration at the first waypoint (main path)
    public float alternatePathSpecialIdleDuration = 10f; // Idle duration at the 5th waypoint (alternate path)

    private int currentWaypointIndex = 0; // Current waypoint index
    private bool canInteract = false; // Whether the player can interact with the NPC
    private bool useAlternatePath = false; // Whether the NPC should follow the alternate path
    private bool isIdle = false; // To check if the NPC is in idle state
    private bool hasSwitchedPath = false; // Whether the path has already been switched

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

        if (canInteract && Input.GetKeyDown(KeyCode.E) && !hasSwitchedPath)
        {
            Debug.Log("Player pressed E to switch NPC to alternate path.");
            SwitchToAlternatePath();
        }

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform targetWaypoint = useAlternatePath ? alternateWaypoints[currentWaypointIndex] : waypoints[currentWaypointIndex];

        if (isIdle)
            return;

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
            if (useAlternatePath && currentWaypointIndex == 4)
            {
                StartCoroutine(PerformSpecialIdleSequence());
            }
            else if (!useAlternatePath)
            {
                StartCoroutine(IdleAtWaypoint(currentWaypointIndex == 0 ? firstWaypointIdleDuration : idleDuration, false));
            }
            else
            {
                currentWaypointIndex++;

                if (currentWaypointIndex >= alternateWaypoints.Length)
                {
                    SwitchToMainPath();
                }
            }
        }
    }

    private IEnumerator IdleAtWaypoint(float duration, bool isAlternatePath)
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
        currentWaypointIndex++;

        if (isAlternatePath)
        {
            if (currentWaypointIndex >= alternateWaypoints.Length)
            {
                SwitchToMainPath();
            }
        }
        else
        {
            currentWaypointIndex %= waypoints.Length;
        }
    }

    private IEnumerator PerformSpecialIdleSequence()
    {
        isIdle = true;

        // Transition from Walking to Idle (1 second)
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
        Debug.Log("NPC is idling...");
        yield return new WaitForSeconds(1f);

        // Transition from Idle to Talking (3 seconds)
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsTalking", true);
        Debug.Log("NPC is talking...");
        yield return new WaitForSeconds(3f);

        // Transition from Talking to Pointing (3 seconds)
        animator.SetBool("IsTalking", false);
        animator.SetBool("IsPointing", true);
        Debug.Log("NPC is pointing...");
        yield return new WaitForSeconds(3f);

        // Transition from Pointing to Talking (5 seconds)
        animator.SetBool("IsPointing", false);
        animator.SetBool("IsTalking", true);
        Debug.Log("NPC is talking again...");
        yield return new WaitForSeconds(5f);

        // Finish sequence and move to the next waypoint
        animator.SetBool("IsTalking", false);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalking", true);
        Debug.Log("NPC finished special idle sequence and is walking to the next waypoint.");
        isIdle = false;
        currentWaypointIndex++;
    }

    private void SwitchToAlternatePath()
    {
        if (alternateWaypoints.Length == 0)
        {
            Debug.LogWarning("No alternate waypoints assigned!");
            return;
        }

        Debug.Log("Switching NPC to alternate path.");
        useAlternatePath = true;
        hasSwitchedPath = true;
        currentWaypointIndex = 0;

        isIdle = false;

        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
        }
    }

    private void SwitchToMainPath()
    {
        Debug.Log("Switching NPC back to the main path.");
        useAlternatePath = false;
        currentWaypointIndex = 0;

        isIdle = false;

        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered NPC's interaction range.");
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited NPC's interaction range.");
            canInteract = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawWaypointPath(waypoints);

        Gizmos.color = Color.blue;
        DrawWaypointPath(alternateWaypoints);
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
