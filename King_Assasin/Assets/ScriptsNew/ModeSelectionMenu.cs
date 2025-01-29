using UnityEngine;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
    public Button leftButton;    // Reference to the left navigation button
    public Button rightButton;   // Reference to the right navigation button
    public Button storyModeButton; // Reference to the Story Mode button
    public Button cruiseModeButton; // Reference to the Cruise Mode button

    private int currentModeIndex = 0; // 0 for Story Mode, 1 for Cruise Mode

    void Start()
    {
        // Set initial button states
        UpdateButtonStates();

        // Add listeners to navigation buttons
        leftButton.onClick.AddListener(PreviousMode);
        rightButton.onClick.AddListener(NextMode);
    }

    void UpdateButtonStates()
    {
        // Enable or disable the buttons based on the current mode index
        if (currentModeIndex == 0)
        {
            storyModeButton.gameObject.SetActive(true);
            cruiseModeButton.gameObject.SetActive(false);
        }
        else
        {
            storyModeButton.gameObject.SetActive(false);
            cruiseModeButton.gameObject.SetActive(true);
        }
    }

    void PreviousMode()
    {
        currentModeIndex = (currentModeIndex - 1 + 2) % 2; // Wrap around between 0 and 1
        UpdateButtonStates();
    }

    void NextMode()
    {
        currentModeIndex = (currentModeIndex + 1) % 2; // Wrap around between 0 and 1
        UpdateButtonStates();
    }
}
