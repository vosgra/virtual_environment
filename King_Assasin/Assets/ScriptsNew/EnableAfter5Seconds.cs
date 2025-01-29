using System.Collections;
using UnityEngine;

public class ActivateAfter5Sec : MonoBehaviour
{
    public GameObject objectToActivate; // Assign the object in the Inspector
    private bool isPlayerInTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            Debug.Log("Player entered trigger.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            Debug.Log("Player exited trigger.");
        }
    }

    void Update()
    {
        // Check if the player is in the trigger and presses the "E" key
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed, starting delay...");
            StartCoroutine(ActivateAfterDelay());
        }
    }

    IEnumerator ActivateAfterDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5);

        // Activate the object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log("Object activated after 5 seconds.");
        }
        else
        {
            Debug.LogWarning("No object assigned to activate.");
        }
    }
}