using UnityEngine;

public class ToggleObjectsAfterTime : MonoBehaviour
{
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private float delay = 5f;

    private void Start()
    {
        StartCoroutine(ToggleObjects());
    }

    private System.Collections.IEnumerator ToggleObjects()
    {
        yield return new WaitForSeconds(delay);

        if (objectToDisable)
            objectToDisable.SetActive(false);

        if (objectToEnable)
            objectToEnable.SetActive(true);
    }
}
