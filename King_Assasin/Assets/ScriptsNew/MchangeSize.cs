using UnityEngine;

public class ScaleOnKeyPress : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(2f, 2f, 2f); // Desired scale size
    public float scaleSpeed = 5f; // Speed of scaling
    private Vector3 originalScale;
    private bool isScaled = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isScaled = !isScaled; // Toggle scaling
        }

        transform.localScale = Vector3.Lerp(transform.localScale, isScaled ? targetScale : originalScale, Time.deltaTime * scaleSpeed);
    }
}