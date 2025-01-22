using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For Image handling

public class RecipeManagerManage : MonoBehaviour
{
    public GameObject recipeImage; // Assign the recipe image GameObject in the Inspector
    public KeyCode interactKey = KeyCode.E; // Key to interact with NPC
    public KeyCode toggleKey = KeyCode.J; // Key to toggle recipe position
    public Transform sidePosition; // Assign the side position Transform in the Inspector
    public Transform middlePosition; // Assign the middle position Transform in the Inspector
    public RawImage displayImage; // Assign the RawImage in the Inspector
    public Texture updatedImage; // Assign the new texture in the Inspector

    private bool isPlayerInRange = false; // Tracks if player is in NPC collider
    private bool isRecipeAcquired = false; // Tracks if the recipe has been acquired
    private bool isRecipeAtMiddle = true; // Tracks recipe position

    private Dictionary<string, int> mushroomCollection = new Dictionary<string, int>(); // Tracks collected mushrooms
    private Dictionary<string, int> requiredMushrooms = new Dictionary<string, int> // Required mushrooms
    {
        { "Red", 2 },
        { "Brown", 2 },
        { "Purple", 1 }
    };

    void Start()
    {
        if (recipeImage != null)
        {
            recipeImage.SetActive(false); // Hide the recipe initially
        }
    }

    void Update()
    {
        if (isPlayerInRange && !isRecipeAcquired && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(AcquireRecipeAfterDelay(12f));
        }

        if (isRecipeAcquired && Input.GetKeyDown(toggleKey))
        {
            ToggleRecipePosition();
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
    }

    private void ToggleRecipePosition()
    {
        if (recipeImage != null)
        {
            if (isRecipeAtMiddle)
            {
                recipeImage.transform.position = sidePosition.position;
            }
            else
            {
                recipeImage.transform.position = middlePosition.position;
            }

            isRecipeAtMiddle = !isRecipeAtMiddle;
        }
    }

    public void CollectMushroom(string mushroomType)
    {
        if (!mushroomCollection.ContainsKey(mushroomType))
        {
            mushroomCollection[mushroomType] = 0;
        }

        mushroomCollection[mushroomType]++;
        CheckMushroomCollection();
    }

    private void CheckMushroomCollection()
    {
        foreach (var required in requiredMushrooms)
        {
            if (!mushroomCollection.ContainsKey(required.Key) || mushroomCollection[required.Key] < required.Value)
            {
                return; // Exit if any required mushroom is not collected sufficiently
            }
        }

        UpdateRecipeImage(); // All required mushrooms are collected
    }

    private void UpdateRecipeImage()
    {
        if (displayImage != null && updatedImage != null)
        {
            displayImage.texture = updatedImage; // Change the RawImage texture
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
