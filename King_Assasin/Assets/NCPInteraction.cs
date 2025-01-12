using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    public Animator npcAnimator; // Reference to the NPC's Animator

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the collider
        if (other.CompareTag("Player"))
        {
            // Trigger the transition to standing
            npcAnimator.SetBool("IsStanding", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Reset to sitting when the player exits
        if (other.CompareTag("Player"))
        {
            npcAnimator.SetBool("IsStanding", false);
        }
    }
}
