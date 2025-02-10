using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    public GameObject objectA; // The object that must remain enabled
    public GameObject objectB; // The object that will be disabled after 19 seconds
    public GameObject objectC; // The object that will be enabled after 19 seconds
    private float timer = 0f;
    public float timewait = 0f;
    private bool isCounting = false;

    void Update()
    {
        // Check if objectA is enabled
        if (objectA != null)
        {
            if (!objectA.activeSelf)
            {
                Debug.Log("Warning: objectA is disabled!");
            }
        }

        // If objectB is enabled, start the countdown
        if (objectB != null && objectB.activeSelf)
        {
            if (!isCounting)
            {
                isCounting = true;
                timer = 0f; // Reset the timer when objectB gets enabled
            }

            timer += Time.deltaTime;

            // After 19 seconds, disable objectB and enable objectC
            if (timer >= timewait)
            {
                objectB.SetActive(false);

                if (objectC != null)
                {
                    objectC.SetActive(true);
                }

                isCounting = false; // Stop counting after switching objects
            }
        }
        else
        {
            isCounting = false; // Reset countdown if objectB is disabled
        }
    }
}
