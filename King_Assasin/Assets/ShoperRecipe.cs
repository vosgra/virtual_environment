using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform Waypoint1; // Set this to the waypoint for GameObject1 in the inspector
    public Transform Waypoint2; // Set this to the waypoint for GameObject2 in the inspector

    public GameObject Recipe; // First game object to check
    public GameObject RecipeComplete; // Second game object to check
    public GameObject SoupMessage; // Third game object to activate
    public GameObject SpeachNotComplete;
    public GameObject CompleteSpeach;
    public GameObject NoRecipeSpeach;
    public GameObject DisableRecipe;
    


    private bool isPlayerInRange = false; // Tracks if the player is inside the collider

    void Update()
    {
        // Check if the player presses E and is within the collider range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(HandleInteraction());
            
        }
    }

    private IEnumerator HandleInteraction()
    {
        // Wait for the specified delay before deactivating the object
        yield return new WaitForSeconds(3);

        // Deactivate the object
        if (Recipe.activeInHierarchy)
        {
            Recipe.transform.position = Waypoint1.position;
            yield return new WaitForSeconds(3);
            Recipe.transform.position = Waypoint2.position;
            //SpeachNotComplete.SetActive(true);

            
        }
         else if (RecipeComplete.activeInHierarchy)
        {

            RecipeComplete.transform.position = Waypoint1.position;
            yield return new WaitForSeconds(3);
            DisableRecipe.SetActive(false);
            SoupMessage.SetActive(true);
            
            //CompleteSpeach.SetActive(true);
            RecipeComplete.SetActive(false);
            yield return new WaitForSeconds(5);
            SoupMessage.SetActive(false);
            

        }

        else if (!RecipeComplete.activeInHierarchy && !Recipe.activeInHierarchy)
        { 
           //NoRecipeSpeach.SetActive(true);
        
        }


    }

      
    


    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the collider
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
