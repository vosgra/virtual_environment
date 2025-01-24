using UnityEngine;

namespace Unity.FantasyKingdom
{
    public class MushroomCounter1 : MonoBehaviour
    {
        public int mushroomcounter = 0;
        public GameObject mush1;
        public GameObject mush2;
        public GameObject mush3;
        public GameObject mush4;
        public GameObject mush5;
        bool mush1counter = true;
        bool mush2counter = true;
        bool mush3counter = true;
        bool mush4counter = true;
        bool mush5counter = true;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {



            if (!mush1.activeInHierarchy && mush1counter)
            {

                mushroomcounter = mushroomcounter + 1;
                mush1counter = false;
            }
            if (!mush2.activeInHierarchy && mush2counter)
            {
                mushroomcounter = mushroomcounter + 1;
                mush2counter = false;
            }
            if (!mush3.activeInHierarchy && mush3counter)
            {
                mushroomcounter = mushroomcounter + 1;
                mush3counter = false;
            }
            if (!mush4.activeInHierarchy && mush4counter)
            {
                mushroomcounter = mushroomcounter + 1;
                mush4counter = false;
            }
            if (!mush5.activeInHierarchy && mush5counter)
            {
                mushroomcounter = mushroomcounter + 1;
                mush5counter = false;
            }

        }
    }
}