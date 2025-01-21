using UnityEngine;
using System.Collections;

public class NPCAnimationControllerRefuge : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public float pointingDuration = 4f; // Time spent in the Pointing state
    public float standingDuration = 5f; // Time spent in the Standing state
    public float talkingingDuration = 7f; // Time spent in the Standing state
    public float waitingDuration = 1f; // Time spent in the Standing state
    public bool alreadyactivated = false;

    private bool playerNearby = false; // Tracks if the player is in range
    private bool performanceStarted = false; // Tracks if the performance has started

    private void Update()
    {
        // Check if the player presses the 'E' key and is within range
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !performanceStarted && alreadyactivated == false)
        {
            StartCoroutine(StartPerformance());
            alreadyactivated = true;
        }
    }

    private IEnumerator StartPerformance()
    {
        performanceStarted = true;

        
        // Transition to Talking
        animator.SetBool("IsPointing", false);
        animator.SetBool("IsTalkingSitting", true);
        animator.SetBool("IsStanding", false);
        yield return new WaitForSeconds(talkingingDuration);

        // Transition to Pointing
        animator.SetBool("IsSitting", false);
        animator.SetBool("IsTalkingSitting", false);
        animator.SetBool("IsPointing", true);
        yield return new WaitForSeconds(pointingDuration);

        // Transition to Talking
        animator.SetBool("IsPointing", false);
        animator.SetBool("IsTalkingSitting", true);
        animator.SetBool("IsStanding", false);
        yield return new WaitForSeconds(talkingingDuration);

        // Transition back to Sitting
        animator.SetBool("IsPointing", false);
        animator.SetBool("IsTalkingSitting", false);
        animator.SetBool("IsSitting", true);

        

        
        performanceStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the collider
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the collider
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
