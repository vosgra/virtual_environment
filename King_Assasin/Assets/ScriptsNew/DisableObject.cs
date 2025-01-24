using UnityEngine;
using System.Collections;

public class DisableObject : MonoBehaviour
{
    [Header("References")]
    public GameObject objectToDisable; // The object to disable
    public Transform teleportWaypoint; // The waypoint to teleport the object to

    [Header("Settings")]
    public float disableTime = 5f; // Time to keep the object disabled (modifiable in Inspector)

    private bool isActivated = false;
    private bool alreadyActivated = false;
    private bool playerInside = false; // Tracks whether the player is inside the NPC's collider

    void Update()
    {
        // Check for the "E" key press, if activation hasn't occurred, and the player is inside the collider
        if (Input.GetKeyDown(KeyCode.E) && !isActivated && !alreadyActivated && playerInside)
        {
            StartCoroutine(HandleObject());
            alreadyActivated = true;
        }
    }

    private IEnumerator HandleObject()
    {
        isActivated = true;

        // Disable the specified object
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }

        // Wait for the specified disable time
        yield return new WaitForSeconds(disableTime);

        // Teleport the object to the waypoint
        if (objectToDisable != null && teleportWaypoint != null)
        {
            objectToDisable.transform.position = teleportWaypoint.position;
        }

        // Re-enable the object
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(true);
        }

        isActivated = false; // Reset activation state
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is the player (tagged as "Player")
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the collider is the player (tagged as "Player")
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
