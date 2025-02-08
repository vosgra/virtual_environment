using System.Collections;
using UnityEngine;

public class ActivateAfter5Sec : MonoBehaviour
{
    public GameObject objectToActivate; // Assign the object in the Inspector
    public float delayTime = 5f; // Editable in Inspector with 5 second default
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
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed, starting delay...");
            StartCoroutine(ActivateAfterDelay());
        }
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); // Use the public variable

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log("Object activated after " + delayTime + " seconds.");
        }
        else
        {
            Debug.LogWarning("No object assigned to activate.");
        }
    }
}