using System.Collections;
using UnityEngine;

public class TimedObjectActivator : MonoBehaviour
{
    public float delayBeforeActivation;// Set delay in Inspector
    [SerializeField] private GameObject[] objectsToEnable;
    [SerializeField] private GameObject[] objectsToDisable;

    private void OnEnable()
    {
        StartCoroutine(ActivateObjectsAfterDelay());
    }

    private IEnumerator ActivateObjectsAfterDelay()
    {
        yield return (delayBeforeActivation);

        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}
