using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject targetObject; // The object to monitor
    public GameObject objectToActivate; // The object to activate/deactivate

    void Update()
    {
        if (targetObject != null && objectToActivate != null)
        {
            objectToActivate.SetActive(targetObject.activeInHierarchy);
        }
    }
}
