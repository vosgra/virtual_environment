using UnityEngine;

namespace Unity.FantasyKingdom
{
    public class textFollowCamera : MonoBehaviour
    {
        // Reference to the player's Transform
        public Transform Player;

        void Update()
        {
            if (Player != null)
            {
                // Rotate the object to face the player
                transform.LookAt(Player);

                // Adjust to ensure the text is facing the player (optional)
                transform.rotation = Quaternion.LookRotation(transform.position - Player.position);
            }
        }
    }
}
