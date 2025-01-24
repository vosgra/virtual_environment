using UnityEngine;

public class ToggleObjectWithM : MonoBehaviour
{
    // Assign the target GameObject in the Inspector
    [SerializeField] private GameObject targetObject;

    void Update()
    {
        // Check if the "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Check if the target object is active
            if (targetObject != null && targetObject.activeSelf)
            {
                // Deactivate the target object
                targetObject.SetActive(false);
            }
        }
    }
}
