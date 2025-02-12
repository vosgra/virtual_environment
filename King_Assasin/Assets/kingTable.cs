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

    [Header("Activation Condition")]
    public GameObject requiredObject; // Object that must be active
    public float activationDelay = 3f; // Time the object must be active before script works

    private bool playerInside = false;
    public GameObject player;
    public GameObject Cutscenetransition;
    public GameObject ui1;
    public GameObject ui2;

    private float activationTimer = 0f;
    private bool isActivated = false;

    private void Update()
    {
        // Check if required object is active and track time
        if (requiredObject.activeSelf)
        {
            isActivated = true;
        }
        

        // Handle input if the player is inside and the script is activated
        if (isActivated && playerInside && (Input.GetKeyDown(button1) || Input.GetKeyDown(button2)))
        {
            objectToEnable.SetActive(true);

            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated && other.CompareTag("Player")) // Only allow trigger if script is activated
        {
            uiObject.SetActive(true);
            playerInside = true;
            player.SetActive(false);
            Cutscenetransition.SetActive(true);
            ui1.SetActive(false);
            ui2.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
