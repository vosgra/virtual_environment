using UnityEngine;
using UnityEngine.UI;

public class TriggerObjectController : MonoBehaviour
{
    [Header("UI/GameObject to Enable on Trigger")]
    public GameObject uiObject;

    [Header("Object to Enable on Button Press")]
    public GameObject objectToEnable;

    [Header("Objects to Disable on Button Press")]
    public GameObject[] objectsToDisable;

    [Header("Button Keys")]
    public KeyCode button1 = KeyCode.Alpha1;
    public KeyCode button2 = KeyCode.Alpha2;

    private bool playerInside = false;
    public GameObject player;
    public GameObject Cutscenetransition;
    public GameObject ui1;
    public GameObject ui2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiObject.SetActive(true);
            playerInside = true;
            player.SetActive(false);
            Cutscenetransition.SetActive(true);
            ui1.SetActive(false);
            ui2.SetActive(false);
        }
    }

    

    private void Update()
    {
        if (playerInside && (Input.GetKeyDown(button1) || Input.GetKeyDown(button2)))
        {
            objectToEnable.SetActive(true);

            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }
}
