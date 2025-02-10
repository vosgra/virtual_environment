using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] private float disableTime = 5f; // Time in seconds before disabling the GameObject

    private void Start()
    {
        Invoke("DisableObject", disableTime);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
