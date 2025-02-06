using UnityEngine;
using UnityEngine.UI;

public class NPCInteractionImage : MonoBehaviour
{
    public GameObject interactionImage; // Assign the UI image in the Inspector
    private bool isPlayerInside = false;
    private bool hasInteracted = false;

    private void Start()
    {
        if (interactionImage != null)
        {
            interactionImage.SetActive(false); // Ensure the image is initially hidden
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            interactionImage.SetActive(true);
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionImage.SetActive(false);
            isPlayerInside = false;
        }
    }

    private void Update()
    {
        if (isPlayerInside && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            interactionImage.SetActive(false);
            hasInteracted = true;
        }
    }
}
