using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleGameObjects12 : MonoBehaviour
{
    [SerializeField] private Button toggleButton; // Reference to the UI button
    [SerializeField] private List<GameObject> objectsToDisable; // List of objects to disable
    [SerializeField] private List<GameObject> objectsToEnable; // List of objects to enable

    private void Start()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleObjects);
        }
    }

    private void ToggleObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}
