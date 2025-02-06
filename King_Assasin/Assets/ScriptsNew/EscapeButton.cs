using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas; // Assign your menu UI GameObject in the Inspector
    public GameObject player; // Assign your player GameObject
    public Camera playerCamera; // Assign the player's camera
    public GameObject Mode;
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && Mode.activeInHierarchy == true)
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        bool isMenuActive = !menuCanvas.activeSelf;

        // Toggle the menu's active state
        menuCanvas.SetActive(isMenuActive);

        // Handle cursor visibility and lock state
        Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuActive;

        // Disable player and camera when the menu is open
        if (player != null)
            player.SetActive(!isMenuActive);

        if (playerCamera != null)
            playerCamera.gameObject.SetActive(!isMenuActive);

        // Pause/unpause the game (optional)
        Time.timeScale = isMenuActive ? 0f : 1f;
    }
}