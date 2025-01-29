using System.Collections;
using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    public GameObject triggeringObject; // The object that triggers the activation
    public GameObject objectToActivate; // The object that will be activated
    public float activationDelay = 1f; // Time to wait after the triggering object is activated
    private bool isTriggeringObjectActivated = false; // Tracks if the triggering object is activated

    private void Update()
    {
        // Check if the triggering object has been activated
        if (triggeringObject.activeInHierarchy && !isTriggeringObjectActivated)
        {
            // Start the coroutine to activate the object after the delay
            isTriggeringObjectActivated = true;
            StartCoroutine(ActivateObjectAfterDelay());
        }
    }

    // Coroutine to activate the object after the specified delay
    private IEnumerator ActivateObjectAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(activationDelay);

        // Activate the object
        objectToActivate.SetActive(true);
        yield return new WaitForSeconds(3f);

        // Deactivate the object
        objectToActivate.SetActive(false);

    }
}
