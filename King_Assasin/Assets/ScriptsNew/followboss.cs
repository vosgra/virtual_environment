using UnityEngine;

public class NPCFollower : MonoBehaviour
{
    public Transform leader;  // The NPC to follow
    public float followDistance = 2f;  // Distance to stay at the left
    public float followSpeed = 5f;  // Speed of following

    public Animator animator;  // Reference to the NPC's Animator
    private bool isIdle = false;  // Tracks if the NPC should be idle

    private Vector3 leaderLastPosition;  // Stores the leader's position from the last frame

    void Start()
    {
        if (leader != null)
        {
            leaderLastPosition = leader.position;
        }
    }

    void Update()
    {
        if (leader == null || isIdle) return;

        // Check if the leader has stopped moving
        CheckLeaderMovement();

        // Calculate the target position on the left of the leader
        Vector3 leftOffset = -leader.right * followDistance;
        Vector3 targetPosition = leader.position + leftOffset;

        // Move smoothly to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Ensure the follower faces the same direction as the leader
        transform.rotation = leader.rotation;
    }

    // Check if the leader has stopped moving
    private void CheckLeaderMovement()
    {
        if (leader.position == leaderLastPosition)
        {
            // Leader has stopped moving, set follower to idle
            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", true);
            }
        }
        else
        {
            // Leader is moving, set follower to walking
            if (animator != null)
            {
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsIdle", false);
            }
        }

        // Update the leader's last position
        leaderLastPosition = leader.position;
    }

    // When the follower enters a specific trigger, it stops and goes idle
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IdleZone"))  // Ensure the collider has the tag "IdleZone"
        {
            isIdle = true;
            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", true);
            }
        }
    }
}