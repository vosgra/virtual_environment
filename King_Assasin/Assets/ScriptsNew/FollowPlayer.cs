using UnityEngine;

public class FollowXZ : MonoBehaviour
{
    // The object to follow
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            // Get the current position of this object
            Vector3 currentPosition = transform.position;

            // Update the x and z positions to match the target, keep y the same
            transform.position = new Vector3(target.position.x, currentPosition.y, target.position.z);
        }
    }
}
