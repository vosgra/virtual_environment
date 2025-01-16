using UnityEngine;

public class NPCAnimationTrigger : MonoBehaviour
{
    // Reference to the Animator component of the NPC
    public Animator npcAnimator;

    // Reference to the player
    public Transform player;

    // Store the initial rotation of the NPC
    private Quaternion initialRotation;

    // Flag to determine if the NPC should look at the player
    private bool shouldLookAtPlayer = false;

    void Start()
    {
        // Cache the initial rotation of the NPC
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Continuously rotate to look at the player if the flag is set
        if (shouldLookAtPlayer)
        {
            LookAtPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is the player
        if (other.CompareTag("Player"))
        {
            // Set the Animator booleans
            npcAnimator.SetBool("IsSitting", false);
            npcAnimator.SetBool("IsIdle", true);

            // Enable the flag to look at the player
            shouldLookAtPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the collider is the player
        if (other.CompareTag("Player"))
        {
            // Reset to sitting state
            npcAnimator.SetBool("IsSitting", true);
            npcAnimator.SetBool("IsIdle", false);

            // Disable the flag and reset rotation
            shouldLookAtPlayer = false;
            ResetRotation();
        }
    }

    void LookAtPlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Ignore the y-axis for rotation

        // Calculate the target rotation to face the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Smoothly rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void ResetRotation()
    {
        // Smoothly reset the rotation to the initial rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 5f);
    }
}
