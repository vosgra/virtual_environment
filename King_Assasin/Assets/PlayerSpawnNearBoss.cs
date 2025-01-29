using UnityEngine;

public class TeleportOnEnterWithKeyPress : MonoBehaviour
{
    public GameObject objectToTeleport; // The GameObject to teleport
    public Transform npcTransform;      // Reference to the NPC's Transform
    public float teleportDistance = 2f; // Distance at which the object will teleport near the NPC

    private bool playerInRange = false; // Flag to check if player is in range

    private void Update()
    {
        // Check if player is inside the trigger zone and presses the "E" key
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Enable the objectToTeleport
            objectToTeleport.SetActive(true);

            // Calculate the new position (teleport near the NPC)
            Vector3 teleportPosition = npcTransform.position + npcTransform.forward * teleportDistance;

            // Teleport the object to the new position
            objectToTeleport.transform.position = teleportPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player (you can modify this as per your requirements)
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has left the trigger zone
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
