using UnityEngine;
using System.Collections.Generic;

namespace Unity.FantasyKingdom
{
    public class MushroomCollected : MonoBehaviour
    {
        public int redMushroomCounter = 0; // Counter for Red mushrooms
        public int brownMushroomCounter = 0; // Counter for Brown mushrooms
        public int purpleMushroomCounter = 0; // Counter for Purple mushrooms

        public List<GameObject> mushrooms = new List<GameObject>(); // List to hold all mushroom GameObjects
        private List<bool> mushroomCounters = new List<bool>(); // Tracks if each mushroom has been counted

        public GameObject objectToDisable; // GameObject to disable when condition is met
        public GameObject objectToEnable; // GameObject to enable when condition is met

        // Start is called before the first execution of Update
        void Start()
        {
            // Initialize mushroomCounters to match the size of the mushrooms list
            foreach (var mushroom in mushrooms)
            {
                mushroomCounters.Add(true); // Mark all mushrooms as countable initially
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Iterate through the list of mushrooms
            for (int i = 0; i < mushrooms.Count; i++)
            {
                // Ensure the mushroom exists, is inactive, and hasn't been counted yet
                if (mushrooms[i] != null && !mushrooms[i].activeInHierarchy && mushroomCounters[i])
                {
                    CountMushroom(mushrooms[i]); // Categorize and count the mushroom
                    mushroomCounters[i] = false; // Mark this mushroom as counted
                }
            }

            // Check if the condition is met to enable/disable objects
            CheckMushroomCollection();
        }

        // Categorize and count the mushroom based on its tag
        private void CountMushroom(GameObject mushroom)
        {
            switch (mushroom.tag)
            {
                case "RedMush":
                    redMushroomCounter++;
                    break;
                case "BrownMush":
                    brownMushroomCounter++;
                    break;
                case "PurpleMush":
                    purpleMushroomCounter++;
                    break;
                default:
                    Debug.LogWarning($"Unknown mushroom tag: {mushroom.tag}");
                    break;
            }
        }

        // Check if the required mushrooms have been collected
        private void CheckMushroomCollection()
        {
            if (redMushroomCounter >= 2 && brownMushroomCounter >= 2 && purpleMushroomCounter >= 1)
            {
                if (objectToDisable != null)
                {
                    objectToDisable.SetActive(false); // Disable the specified GameObject
                }
                if (objectToEnable != null)
                {
                    objectToEnable.SetActive(true); // Enable the specified GameObject
                }
            }
        }
    }
}
