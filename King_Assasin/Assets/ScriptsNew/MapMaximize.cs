using UnityEngine;

public class ModifySizeAndPosition : MonoBehaviour
{
    // Variables to store the original and modified size and position
    private Vector3 originalPosition;
    private Vector3 originalScale;

    public Vector3 modifiedPosition = new Vector3(2f, 2f, 2f); // Example modified position
    public Vector3 modifiedScale = new Vector3(2f, 2f, 2f);    // Example modified scale

    private bool isModified = false; // State toggle

    void Start()
    {
        // Store the original position and scale
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Check if the M key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleSizeAndPosition();
        }
    }

    private void ToggleSizeAndPosition()
    {
        if (isModified)
        {
            // Revert to the original position and scale
            transform.position = originalPosition;
            transform.localScale = originalScale;
        }
        else
        {
            // Change to the modified position and scale
            transform.position = modifiedPosition;
            transform.localScale = modifiedScale;
        }

        // Toggle the state
        isModified = !isModified;
    }
}
