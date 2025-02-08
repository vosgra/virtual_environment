using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For Image handling

public class RecipeManager : MonoBehaviour
{
    public GameObject recipeImage; // Assign the recipe image GameObject in the Inspector
    public GameObject secondaryObject; // Assign the secondary object in the Inspector
    public KeyCode interactKey = KeyCode.E; // Key to interact with NPC
    public KeyCode toggleKey = KeyCode.J; // Key to toggle recipe position
    public Transform sidePosition; // Assign the side position Transform in the Inspector
    public Transform middlePosition; // Assign the middle position Transform in the Inspector
    public int timetowait;
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
    }

    void Update()
    {
        if (isPlayerInRange && !isRecipeAcquired && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(AcquireRecipeAfterDelay(timetowait));
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
