using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array to hold all cameras
    private int currentCameraIndex = 0;

    void Start()
    {
        // Disable all cameras except the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Disable the current camera
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Increment the index (loop back to 0 if at the end)
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Enable the next camera
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
