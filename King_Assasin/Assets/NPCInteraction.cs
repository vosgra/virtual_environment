using UnityEngine;

public class NPCAnimationTrigger : MonoBehaviour
{
    // Reference to the Animator component of the NPC
    public Animator npcAnimator;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is the player
        if (other.CompareTag("Player"))
        {
            // Set the Animator booleans
            npcAnimator.SetBool("IsSitting", false);
            npcAnimator.SetBool("IsIdle", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Optional: Reset to sitting state when the player exits
        if (other.CompareTag("Player"))
        {
            npcAnimator.SetBool("IsSitting", true);
            npcAnimator.SetBool("IsIdle", false);
        }
    }
}
