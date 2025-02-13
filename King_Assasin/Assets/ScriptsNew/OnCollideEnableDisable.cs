using UnityEngine;

public class EnableDisableOnTrigger : MonoBehaviour
{
    public GameObject objectToEnable; // Assign the object to enable/disable
    public string targetTag = "Player"; // Set the tag of the object to check

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check if the colliding object has the correct tag
        {
            objectToEnable.SetActive(true); // Enable the object
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag)) // Check if the exiting object has the correct tag
        {
            objectToEnable.SetActive(false); // Disable the object
        }
    }
}
