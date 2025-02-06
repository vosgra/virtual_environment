using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public GameObject menuCanvas; // Assign in Inspector
    public GameObject player;     // Assign your player GameObject
    public Camera playerCamera;   // Assign the player's camera

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // Call this method when the "Continue" button is clicked
    public void ToggleMenu()
    {
        bool isMenuActive = !menuCanvas.activeSelf;

        // Toggle the menu
        menuCanvas.SetActive(isMenuActive);

        // Handle cursor
        Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuActive;

        // Toggle player and camera
        if (player != null)
            player.SetActive(!isMenuActive);
        if (playerCamera != null)
            playerCamera.gameObject.SetActive(!isMenuActive);

        // Pause/unpause
        Time.timeScale = isMenuActive ? 0f : 1f;
    }
}