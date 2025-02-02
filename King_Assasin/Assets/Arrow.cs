using UnityEngine;
using UnityEngine.UI;

public class CompassArrowController : MonoBehaviour
{
    public Transform[] targets;              // List of targets to follow
    public Transform player;                 // Reference to the player
    public RectTransform arrow;              // UI arrow to point towards targets
    public Camera requiredCamera;            // Specific camera that must be enabled
    public GameObject requiredObject;        // Specific object that must be enabled

    private int currentTargetIndex = 0;      // Index of the current target

    void Update()
    {
        if (requiredCamera.enabled && requiredObject.activeInHierarchy)
        {
            arrow.gameObject.SetActive(true);
            UpdateArrowDirection();
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
    }

    void UpdateArrowDirection()
    {
        if (targets.Length == 0) return;

        Vector3 direction = targets[currentTargetIndex].position - player.position;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        arrow.localRotation = Quaternion.Euler(0, 0, -angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (other.transform == targets[i])
            {
                SwitchToNextTarget();
                break;
            }
        }
    }

    void SwitchToNextTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
    }
}
