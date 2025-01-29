using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject objectToActivate;  // The first GameObject to activate
    public GameObject secondObjectToActivate; // The second GameObject to activate
    public Transform teleportLocation;  // The location to teleport the player
    public GameObject playerGameObject; // The root Player GameObject to disable
    public string npcIdentifier;        // Unique identifier for this NPC
    public string targetNPCIdentifier;  // The identifier of the specific NPC to interact with

    private bool isNearNPC = false;     // Whether the player is near the NPC
    private GameObject player;          // Reference to the player

    void Update()
    {
        // Check for player input if near the correct NPC
        if (isNearNPC && Input.GetKeyDown(KeyCode.E))
        {
            // Activate the first GameObject when E is pressed
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            // Activate the second GameObject when E is pressed
            if (secondObjectToActivate != null)
            {
                secondObjectToActivate.SetActive(true);
            }

            // Teleport the player
            if (player != null && teleportLocation != null)
            {
                player.transform.position = teleportLocation.position;
            }

            // Disable the player's entire GameObject for a certain time
            if (playerGameObject != null)
            {
                StartCoroutine(DisableGameObjectTemporarily());
            }
        }
    }

    // Detect when player enters NPC's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure it's the player
        {
            player = other.gameObject; // Assign the player reference

            // Check if this NPC matches the target NPC identifier
            if (npcIdentifier == targetNPCIdentifier)
            {
                isNearNPC = true;
            }
        }
    }

    // Detect when player exits NPC's trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure it's the player
        {
            isNearNPC = false;
            player = null; // Clear the player reference
        }
    }

    // Coroutine to disable the player's GameObject for a certain time
    private System.Collections.IEnumerator DisableGameObjectTemporarily()
    {
        playerGameObject.SetActive(false); // Disable the entire GameObject
        yield return new WaitForSeconds(54); // Wait for 54 seconds (or any desired time)
        playerGameObject.SetActive(true); // Re-enable the GameObject
    }
}