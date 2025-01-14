using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Animator npcAnimator; // Reference to the NPC's Animator
    public Transform player; // Reference to the player's Transform

    private bool hasSaluted = false; // Flag to ensure saluting happens only once

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Trigger the salute animation if not already done
            if (!hasSaluted)
            {
                npcAnimator.SetBool("IsIdle", false);
                npcAnimator.SetBool("IsSalute", true);
                hasSaluted = true;
                Invoke("ReturnToIdle", 2f); // Schedule return to idle after 2 seconds
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset saluting logic when the player exits the collider
        if (other.CompareTag("Player"))
        {
            npcAnimator.SetBool("IsSalute", false);
            npcAnimator.SetBool("IsIdle", true);
        }
    }

    private void Update()
    {
        // Ensure the NPC looks at the player when they are close and has saluted
        if (hasSaluted)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }

    private void ReturnToIdle()
    {
        // Transition back to idle animation
        npcAnimator.SetBool("IsSalute", false);
        npcAnimator.SetBool("IsIdle", true);
    }
}