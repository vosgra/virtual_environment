using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineController : MonoBehaviour
{
    [Header("Playable Directors")]
    public PlayableDirector loopingTimeline; // The looping timeline
    public PlayableDirector nextCutsceneTimeline; // The next cutscene timeline

    [Header("UI Elements")]
    public Button startButton; // Reference to the Start button

    private void Start()
    {
        // Ensure the looping timeline plays in a loop
        if (loopingTimeline != null)
        {
            loopingTimeline.stopped += RestartLoopingTimeline;
            loopingTimeline.Play();
        }

        // Add a listener to the Start button
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonPressed);
        }
    }

    private void RestartLoopingTimeline(PlayableDirector director)
    {
        if (director == loopingTimeline && loopingTimeline != null)
        {
            loopingTimeline.time = 0;
            loopingTimeline.Play();
        }
    }

    private void OnStartButtonPressed()
    {
        // Stop the looping timeline
        if (loopingTimeline != null)
        {
            loopingTimeline.stopped -= RestartLoopingTimeline;
            loopingTimeline.Stop();
        }

        // Play the next cutscene timeline
        if (nextCutsceneTimeline != null)
        {
            nextCutsceneTimeline.Play();
        }

        // Disable the Start button (optional)
        if (startButton != null)
        {
            startButton.interactable = false;
        }
    }

    private void OnDestroy()
    {
        // Cleanup event subscriptions
        if (loopingTimeline != null)
        {
            loopingTimeline.stopped -= RestartLoopingTimeline;
        }

        if (startButton != null)
        {
            startButton.onClick.RemoveListener(OnStartButtonPressed);
        }
    }
}
