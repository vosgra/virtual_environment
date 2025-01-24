using UnityEngine;
using System.Collections.Generic;

namespace Unity.FantasyKingdom
{
    public class MushroomCounter : MonoBehaviour
    {
        public int mushroomCounter = 0;
        public List<GameObject> mushrooms = new List<GameObject>(); // List of GameObjects for mushrooms
        internal int mushroomcounter;
        private List<bool> mushroomCounters = new List<bool>(); // List to track if the mushroom was counted

        // Start is called before the first execution of Update
        void Start()
        {
            // Initialize mushroomCounters to match the size of the mushrooms list
            for (int i = 0; i < mushrooms.Count; i++)
            {
                mushroomCounters.Add(true); // Set all mushrooms as countable initially
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Iterate through the list of mushrooms
            for (int i = 0; i < mushrooms.Count; i++)
            {
                if (mushrooms[i] != null && !mushrooms[i].activeInHierarchy && mushroomCounters[i])
                {
                    mushroomCounter++;
                    mushroomCounters[i] = false; // Mark the mushroom as already counted
                }
            }
        }
    }
}
