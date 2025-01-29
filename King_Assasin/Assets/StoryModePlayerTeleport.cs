using UnityEngine;

public class ObjectEnableHandler : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The object to watch for being enabled.")]
    public GameObject targetObject;

    [Tooltip("The object to enable after the delay.")]
    public GameObject objectToEnable;

    [Tooltip("The GameObject that defines the target position for teleportation.")]
    public GameObject teleportTarget;

    [Header("Settings")]
    [Tooltip("Time delay before enabling the object and teleporting.")]
    public float delayTime = 2.0f;

    private bool hasTriggered = false; // Ensure the logic executes only once

    void Update()
    {
        // Check if the target object is enabled and the logic hasn't been triggered yet
        if (targetObject.activeSelf && !hasTriggered)
        {
            hasTriggered = true; // Prevent this block from running multiple times
            StartCoroutine(HandleObjectEnable());
        }
    }

    private System.Collections.IEnumerator HandleObjectEnable()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        // Enable the specified object
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);

            // Teleport the object to enable to the teleport target position
            if (teleportTarget != null)
            {
                objectToEnable.transform.position = teleportTarget.transform.position;
            }
        }
    }
}
