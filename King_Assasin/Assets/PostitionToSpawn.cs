using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // Needed for List

public class PlaceSelector : MonoBehaviour
{
    public Button leftButton;    // Reference to the left navigation button
    public Button rightButton;   // Reference to the right navigation button
    public List<Button> modeButtons = new List<Button>(); // Assign 5 buttons in inspector

    private int currentModeIndex = 0; // Now ranges from 0 to 4

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
        // Activate only the current mode button and deactivate others
        for (int i = 0; i < modeButtons.Count; i++)
        {
            modeButtons[i].gameObject.SetActive(i == currentModeIndex);
        }
    }

    void PreviousMode()
    {
        // Decrement index with wrap-around
        currentModeIndex = (currentModeIndex - 1 + modeButtons.Count) % modeButtons.Count;
        UpdateButtonStates();
    }

    void NextMode()
    {
        // Increment index with wrap-around
        currentModeIndex = (currentModeIndex + 1) % modeButtons.Count;
        UpdateButtonStates();
    }
}