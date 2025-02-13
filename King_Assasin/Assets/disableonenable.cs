using UnityEngine;

public class DisableOnEnable : MonoBehaviour
{
    [Header("Disable Settings")]
    [Tooltip("The game object that will be disabled when this object is enabled.")]
    public GameObject objectToDisable; // The object to disable

    private void OnEnable()
    {
        // Check if the object to disable is assigned
        if (objectToDisable != null)
        {
            // Disable the target object
            objectToDisable.SetActive(false);
            Debug.Log($"{objectToDisable.name} has been disabled because {gameObject.name} was enabled.");
        }
        else
        {
            Debug.LogWarning("Object to disable is not assigned.");
        }
    }
}