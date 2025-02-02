using UnityEngine;
using System.Collections;

public class ActivateCharacter : MonoBehaviour
{
    public GameObject objectToActivate; // The object to activate
    public float enableDuration = 5f;   // Time the object stays enabled
    public float disableDelay = 3f;     // Delay before disabling the object

    private bool playerNearby = false;  // Tracks if the player is in range
    private bool alreadyActivated = false;

    private void Update()
    {
        // Check if the player presses the 'E' key and is within range
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !alreadyActivated)
        {
            ActivateObject();
            alreadyActivated = true;
        }
    }

    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // Activates the object
            StartCoroutine(DeactivateAfterDelay()); // Starts the coroutine to deactivate
        }
        else
        {
            Debug.LogWarning("No object assigned to activate.");
        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(enableDuration); // Waits for the enable duration
        yield return new WaitForSeconds(disableDelay);   // Waits for the disable delay

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // Deactivates the object
            alreadyActivated = false;          // Allows reactivation
        }
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
