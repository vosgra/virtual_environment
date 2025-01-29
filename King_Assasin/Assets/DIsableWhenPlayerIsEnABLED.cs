using UnityEngine;

public class ToggleGameObjects : MonoBehaviour
{
    public GameObject objectToDisable; // Reference to the GameObject that will be disabled
    public GameObject objectToEnable;  // Reference to the GameObject that will be enabled

    private bool hasBeenEnabled = false; // Track if the object has been enabled

    private void Update()
    {
        // Check if this GameObject is active and the action hasn't been performed yet
        if (gameObject.activeSelf && !hasBeenEnabled)
        {
            // Disable the other GameObject
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // Enable the other GameObject (if specified)
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }

            // Mark that the action has been performed
            hasBeenEnabled = true;
        }
    }
}