using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Tooltip("The object to follow")]
    public GameObject target;

    private Vector3 lastPosition;
    private bool isTargetActive = true;

    void Update()
    {
        if (target != null)
        {
            // Check if the target is active
            if (target.activeInHierarchy)
            {
                // Follow the target
                transform.position = target.transform.position;
                transform.rotation = target.transform.rotation;
                lastPosition = transform.position; // Store the last position
                isTargetActive = true;
            }
            else
            {
                // The target is inactive, keep the follower at the last position
                if (isTargetActive)
                {
                    lastPosition = transform.position; // Store the last position
                    isTargetActive = false;
                }
                transform.position = lastPosition; // Stay at the last position until the target is active again
            }
        }
    }
}
