using UnityEngine;

public class GameObjectToggle : MonoBehaviour
{
    public GameObject targetObject; // The GameObject that triggers the change when enabled
    public GameObject objectToEnable; // The GameObject to enable when targetObject is enabled
    public GameObject objectToDisable; // The GameObject to disable when targetObject is enabled

    private void Update()
    {
        // Check if the targetObject is enabled
        if (targetObject != null && targetObject.activeInHierarchy)
        {
            // Enable the specified GameObject
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }

            // Disable the specified GameObject
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }

            // Optionally, disable this script to prevent it from running every frame
            enabled = false;
        }
    }
}