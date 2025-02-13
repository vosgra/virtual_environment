using System.Collections;
using UnityEngine;

public class EnableObjectsAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay = 2f; // Delay before enabling objects
    [SerializeField] private GameObject[] objectsToEnable; // Array of objects to enable

    private void OnEnable()
    {
        StartCoroutine(EnableObjectsWithDelay());
    }

    private IEnumerator EnableObjectsWithDelay()
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
