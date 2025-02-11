using UnityEngine;

public class NPCWaypointAnimationTrigger : MonoBehaviour
{
    public Transform targetWaypoint; // Assign the specific waypoint's transform in the Inspector
    public string walkingParam = "IsWalking"; // Name of the walking animation parameter
    public string idleParam = "IsIdle"; // Name of the idle animation parameter

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the NPC!");
        }

        if (targetWaypoint == null)
        {
            Debug.LogError("Target waypoint is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the NPC entered the collider of the target waypoint
        if (other.transform == targetWaypoint)
        {
            // Change animation state to idle
            animator.SetBool(walkingParam, false);
            animator.SetBool(idleParam, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Change back to walking animation when leaving the waypoint's collider
        if (other.transform == targetWaypoint)
        {
            animator.SetBool(walkingParam, true);
            animator.SetBool(idleParam, false);
        }
    }
}