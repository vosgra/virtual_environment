using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    [SerializeField] private Transform target; // The target object to copy from
    [SerializeField] private bool copyPosition = true;
    [SerializeField] private bool copyRotation = true;
    [SerializeField] private bool copyScale = false;

    void Update()
    {
        if (target != null)
        {
            if (copyPosition)
            {
                Vector3 newPosition = transform.position;
                newPosition.x = target.position.x;
                newPosition.z = target.position.z;
                transform.position = newPosition;
            }

            if (copyRotation)
                transform.rotation = target.rotation;

            if (copyScale)
                transform.localScale = target.localScale;
        }
    }
}