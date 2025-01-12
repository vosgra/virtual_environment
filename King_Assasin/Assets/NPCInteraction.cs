using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Animator npcAnimator;  // Reference to the NPC's Animator
    public string triggerName = "StandUpTrigger"; // The name of the trigger in the Animator

    // This function is called when the player collides with the NPC
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger the animation transition to standing idle
            npcAnimator.SetTrigger(triggerName);
        }
    }
}
