using UnityEngine;
using System.Collections.Generic;

public class DisableObjectsOnEnable : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDisable;

    private void OnEnable()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
