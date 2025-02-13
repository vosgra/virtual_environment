using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    public GameObject objectA; // First required object
    public GameObject objectB; // Second required object
    public GameObject objectC; // Object to enable when conditions are met
    private float timer = 0f;
    public float timewait = 19f;
    private bool isCounting = false;

    void Update()
    {
        // Ensure Object A and Object B are assigned
        if (objectA == null || objectB == null || objectC == null)
        {
            Debug.LogWarning("One or more objects are not assigned in ObjectStateManager.");
            return;
        }

        // Start countdown only if BOTH objectA and objectB are enabled
        if (objectA.activeSelf && objectB.activeSelf)
        {
            if (!isCounting)
            {
                isCounting = true;
                timer = 0f; // Reset the timer when both objects are enabled
            }

            timer += Time.deltaTime;

            // If the countdown reaches the time limit, enable objectC and disable objectB
            if (timer >= timewait)
            {
                objectB.SetActive(false);
                objectC.SetActive(true);
                isCounting = false; // Stop counting after switching objects
            }
        }
        else
        {
            // Reset countdown if either objectA or objectB is disabled
            isCounting = false;
            timer = 0f;
        }
    }
}
