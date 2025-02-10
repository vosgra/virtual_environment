using System.Collections;
using UnityEngine;

public class EnableAfterDelay : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // Assign the GameObject to enable
    [SerializeField] private float delay = 2f; // Set the delay time in seconds

    private void OnEnable()
    {
        StartCoroutine(EnableTargetAfterDelay());
    }

    private IEnumerator EnableTargetAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}
