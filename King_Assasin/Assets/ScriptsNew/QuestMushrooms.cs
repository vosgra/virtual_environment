using UnityEngine;
using UnityEngine.UI;

namespace Unity.FantasyKingdom
{
    public class QuestMushrooms : MonoBehaviour
    {
        public MushroomCounter mushroomcounter; // Reference to the MushroomCounter script
        public Text text;

        void Start()
        {
            // Ensure that mushroomcounter is assigned, either in the Inspector or dynamically
            if (mushroomcounter == null)
            {
                mushroomcounter = FindObjectOfType<MushroomCounter>(); // Finds MushroomCounter in the scene
            }
        }

        void Update()
        {
            // Access the mushroomcounter value from the MushroomCounter script
            text.text = "Mushrooms\nCollected:\n" + mushroomcounter.mushroomcounter + "/5";
            if (mushroomcounter.mushroomcounter >= 5)
            {
                text.color = Color.green; // Set color to green if all mushrooms are collected
            }
            else
            {
                text.color = Color.red; // Set color to red if not all mushrooms are collected
            }
        }
    }
}
