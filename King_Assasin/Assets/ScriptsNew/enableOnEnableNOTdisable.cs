using UnityEngine;

public class EnableOnEnableNOTDisable : MonoBehaviour
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
        
    }

   
}
