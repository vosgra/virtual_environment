using UnityEngine;

public class ActivateObjectOnInteraction : MonoBehaviour
{
    public GameObject objectToActivate; // The object to activate
    private bool playerNearby = false; // Tracks if the player is in range
    private bool alreadyActivated = false;

    private void Update()
    {
        // Check if the player presses the 'E' key and is within range
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && alreadyActivated==false)
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
        }
        else
        {
            Debug.LogWarning("No object assigned to activate.");
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
