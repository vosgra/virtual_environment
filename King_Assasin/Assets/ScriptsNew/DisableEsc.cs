using UnityEngine;
using System.Collections.Generic;

public class DisableOnObjectsEnabled : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToMonitor;
    [SerializeField] private Behaviour scriptToDisable;

    void Update()
    {
        bool anyEnabled = objectsToMonitor.Exists(obj => obj.activeInHierarchy);
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = !anyEnabled;
        }
    }
}
