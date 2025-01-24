using System.Collections;
using UnityEngine;

public class ActivateAfter5Sec : MonoBehaviour
{
    public GameObject objectToActivate; // Assign the object in the Inspector

    void Update()
    {
        void OnTriggerEnter(Collider other)
        { 
        // Check if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            Debug.Log("E pressed, starting delay...");
            StartCoroutine(ActivateAfterDelay());
        }
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
