using UnityEngine;

public class ObjectActivationControl : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // The object to activate/deactivate

    private void OnEnable()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Activate the target object when this object is activated
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false); // Deactivate the target object when 'J' is pressed
            }
        }
    }

    private void OnDisable()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false); // Optionally deactivate the target object when this object is deactivated
        }
    }
}
