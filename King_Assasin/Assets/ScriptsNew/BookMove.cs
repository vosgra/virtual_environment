using UnityEngine;
using System.Collections;

public class NPCInteractionww : MonoBehaviour
{
    [Header("References")]
    public GameObject targetObject; // The object to activate and move
    public Transform waypoint;     // The waypoint the object will move to

    [Header("Settings")]
    public float moveSpeed = 2f;   // Speed at which the object moves to the waypoint
    public float activeTime = 5f; // Time before the object disappears (modifiable in Inspector)

    private bool playerInRange = false;
    private bool isActivated = false;
    private bool alreadyActivated = false;

    void Update()
    {
        // Check for the "E" key press and if the player is in range
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isActivated && alreadyActivated== false )
        {
            StartCoroutine(ActivateObject());
            alreadyActivated = true;
        }
    }

    private IEnumerator ActivateObject()
    {
        isActivated = true;

        // Activate the object
        targetObject.SetActive(true);

        // Wait for 3 seconds
        yield return new WaitForSeconds(9f);

        // Move the object to the waypoint
        while (Vector3.Distance(targetObject.transform.position, waypoint.position) > 0.1f)
        {
            targetObject.transform.position = Vector3.MoveTowards(
                targetObject.transform.position,
                waypoint.position,
                moveSpeed * Time.deltaTime 
            );
            yield return null; // Wait until the next frame
        }

        // Wait for the specified active time before deactivating the object
        yield return new WaitForSeconds(activeTime);

        // Deactivate or destroy the object
        targetObject.SetActive(false);
        // Optionally, destroy the object: Destroy(targetObject);

        isActivated = false; // Reset activation state
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the collider
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the collider
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
