using UnityEngine;

public class StoryModeActivator : MonoBehaviour
{
    [Header("Objects to Deactivate")]
    public GameObject[] objectsToDeactivate; // Objects to deactivate

    [Header("Objects to Activate")]
    public GameObject[] objectsToActivate; // Objects to activate

    // Call this method when the button is clicked
    public void ActivateStoryMode()
    {
        // Deactivate all objects in the 'objectsToDeactivate' array
        if (objectsToDeactivate != null)
        {
            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }

        // Activate all objects in the 'objectsToActivate' array
        if (objectsToActivate != null)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
