using UnityEngine;

public class SoldierWatchPlayer : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed at which the soldier rotates
    private Transform player;
    private bool shouldRotate = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            player = other.transform;
            shouldRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldRotate = false;
        }
    }

    private void Update()
    {
        if (shouldRotate && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Keep the rotation horizontal
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
