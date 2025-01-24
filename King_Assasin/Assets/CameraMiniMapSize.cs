using UnityEngine;

public class ModifyCameraSize : MonoBehaviour
{
    // Variables to store the original and modified sizes of the orthographic camera
    private float originalSize;
    public float modifiedSize = 10f; // Example modified size

    private bool isModified = false; // State toggle

    private Camera cameraComponent;

    void Start()
    {
        // Get the Camera component and store the original size
        cameraComponent = GetComponent<Camera>();

        if (cameraComponent != null && cameraComponent.orthographic)
        {
            originalSize = cameraComponent.orthographicSize;
        }
        else
        {
            Debug.LogError("This script requires an Orthographic Camera attached to the GameObject.");
        }
    }

    void Update()
    {
        // Check if the M key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleCameraSize();
        }
    }

    private void ToggleCameraSize()
    {
        if (cameraComponent == null) return;

        if (isModified)
        {
            // Revert to the original size
            cameraComponent.orthographicSize = originalSize;
        }
        else
        {
            // Change to the modified size
            cameraComponent.orthographicSize = modifiedSize;
        }

        // Toggle the state
        isModified = !isModified;
    }
}
