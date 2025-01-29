using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For Image handling

public class deactivateRecipeManager : MonoBehaviour
{
    public GameObject recipeImage; // Assign the recipe image GameObject in the Inspector
    public GameObject secondaryObject; // Assign the secondary object in the Inspector
    public GameObject object1; // The first object to activate
    public GameObject object2; // The second object to activate
    public KeyCode interactKey = KeyCode.E; // Key to interact with NPC
    public KeyCode toggleKey = KeyCode.J; // Key to toggle recipe position
    public Transform sidePosition; // Assign the side position Transform in the Inspector
    public Transform middlePosition; // Assign the middle position Transform in the Inspector

    private bool isPlayerInRange = false; // Tracks if player is in NPC collider
    private bool isRecipeAcquired = false; // Tracks if the recipe has been acquired
    private bool isRecipeAtMiddle = true; // Tracks recipe position

    void Start()
    {
        if (recipeImage != null)
        {
            recipeImage.SetActive(false); // Hide the recipe initially
        }

        if (secondaryObject != null)
        {
            secondaryObject.SetActive(false); // Hide the secondary object initially
        }

        if (object1 != null)
        {
            object1.SetActive(false); // Ensure object1 is hidden initially
        }

        if (object2 != null)
        {
            object2.SetActive(false); // Ensure object2 is hidden initially
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isRecipeAcquired && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(AcquireRecipeAfterDelay(12f));
        }

        if (isRecipeAcquired && Input.GetKeyDown(interactKey))
        {
            DisableRecipe(); // Disable the current recipe and activate new objects
        }

        if (isRecipeAcquired && Input.GetKeyDown(toggleKey))
        {
            TogglePosition();
        }
    }

    private IEnumerator AcquireRecipeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isPlayerInRange) // Ensure player is still in range after delay
        {
            AcquireRecipe();
        }
    }

    private void AcquireRecipe()
    {
        if (isRecipeAcquired) return;

        isRecipeAcquired = true;
        if (recipeImage != null)
        {
            recipeImage.SetActive(true); // Show the recipe image
            recipeImage.transform.position = middlePosition.position; // Start in the middle position
            isRecipeAtMiddle = true;
        }

        if (secondaryObject != null)
        {
            // secondaryObject.SetActive(true); // Show the secondary object
            secondaryObject.transform.position = middlePosition.position; // Start in the middle position
        }
    }

    private void DisableRecipe()
    {
        if (recipeImage != null && recipeImage.activeSelf)
        {
            // Move recipeImage to middle and wait for 3 seconds
            StartCoroutine(MoveToMiddleAndWait(recipeImage));
        }
        else if (secondaryObject != null && secondaryObject.activeSelf)
        {
            secondaryObject.SetActive(false); // Disable the secondary object
            object2.SetActive(true); // Activate object2
        }
    }

    private IEnumerator MoveToMiddleAndWait(GameObject activeObject)
    {
        // Move to middle position
        activeObject.transform.position = middlePosition.position;

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // If the recipe image is still active, move it back to the side position
        if (recipeImage != null && recipeImage.activeSelf)
        {
            if (isRecipeAcquired)
            {
                recipeImage.SetActive(false); // Disable the recipe image
            }
            else
            {
                recipeImage.transform.position = sidePosition.position; // Move it back to the side position
            }
        }
    }

    private void TogglePosition()
    {
        if (recipeImage != null && secondaryObject != null)
        {
            if (isRecipeAtMiddle)
            {
                recipeImage.transform.position = sidePosition.position;
                secondaryObject.transform.position = sidePosition.position;
            }
            else
            {
                recipeImage.transform.position = middlePosition.position;
                secondaryObject.transform.position = middlePosition.position;
            }

            isRecipeAtMiddle = !isRecipeAtMiddle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
