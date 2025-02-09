using UnityEngine;

public class ActivateOnDisable : MonoBehaviour
{
    public GameObject targetObject; // The object to monitor
    public GameObject objectToActivate; // The object to activate after disabling
    private bool wasActivated = false;

    void Update()
    {
        if (targetObject != null && objectToActivate != null)
        {
            if (targetObject.activeSelf)
            {
                wasActivated = true; // Mark that the object was activated
            }
            else if (wasActivated)
            {
                objectToActivate.SetActive(true); // Activate the second object when the first is disabled
                wasActivated = false; // Reset flag
            }
        }
    }
}
