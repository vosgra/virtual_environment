using UnityEngine;

namespace Unity.FantasyKingdom
{
    public class ManitariaCollider : MonoBehaviour
    {
        public GameObject textObject;
        public GameObject textObject1;
        public GameObject textObject2;
        public GameObject textObject3;
        private bool isPlayerInRange = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {
                // Make the object disappear
                gameObject.SetActive(false);

            }
            if (isPlayerInRange)
            {

                textObject.SetActive(true);
                textObject3.SetActive(true);
                textObject2.SetActive(true);
                textObject1.SetActive(true);


            }
            else
            {
                textObject.SetActive(false);
                textObject3.SetActive(false);
                textObject2.SetActive(false);
                textObject1.SetActive(false);
            }
        }
        private void OnTriggerEnter(Collider other) {

            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;
                
            }
            

        }
        private void OnTriggerStay(Collider other)
        {

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;
            }
           
        }
    }
}
