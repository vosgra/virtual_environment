using UnityEngine;

public class TeleportOnActivate : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("The game object that will be teleported.")]
    public GameObject objectToTeleport; // The object to teleport

    [Tooltip("The target game object where the object will be teleported to.")]
    public GameObject targetObject; // The target object to teleport to

    private void OnEnable()
    {
        // Check if both objects are assigned
        if (objectToTeleport != null && targetObject != null)
        {
            // Teleport the object to the target's position
            objectToTeleport.transform.position = targetObject.transform.position;
            Debug.Log($"{objectToTeleport.name} has been teleported to {targetObject.name}'s position.");
        }
        else
        {
            Debug.LogWarning("Object to teleport or target object is not assigned.");
        }
    }
}