using UnityEngine;
using UnityEngine.UI;

public class TeleportOnClick : MonoBehaviour
{
    public GameObject objectToTeleport; // The object that will be teleported
    public GameObject targetLocation; // The target location
    public Button teleportButton; // The button to trigger teleportation

    void Start()
    {
        if (teleportButton != null)
        {
            teleportButton.onClick.AddListener(Teleport);
        }
    }

    void Teleport()
    {
        if (objectToTeleport != null && targetLocation != null)
        {
            objectToTeleport.transform.position = targetLocation.transform.position;
        }
        else
        {
            Debug.LogError("Teleport failed: Object or target location is not assigned.");
        }
    }
}
