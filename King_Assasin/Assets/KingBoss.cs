using UnityEngine;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [System.Serializable]
    public class TextSection
    {
        public GameObject textObject;
        public float duration = 5f;
    }

    [System.Serializable]
    public class AudioSection
    {
        public GameObject audioObject;
        public float delayBeforeNext = 1f;
        public float duration = 5f;
    }

    public TextSection[] textSections;
    public AudioSection[] audioSections;
    public GameObject[] dialogueObjects;
    public float dialogueObjectDuration = 3f;

    private bool isPlaying = false;

    void OnEnable()
    {
        if (!isPlaying)
        {
            StartCoroutine(PlayMonologue());
        }
    }

    IEnumerator PlayMonologue()
    {
        isPlaying = true;

        // Enable dialogue objects
        foreach (var obj in dialogueObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        StartCoroutine(DisableDialogueObjectsAfterDelay(dialogueObjectDuration));

        // Play text sections sequentially
        foreach (var section in textSections)
        {
            if (section.textObject != null)
                section.textObject.SetActive(true);

            yield return new WaitForSeconds(section.duration);

            if (section.textObject != null)
                section.textObject.SetActive(false);
        }

        // Play audio sections sequentially
        foreach (var section in audioSections)
        {
            if (section.audioObject != null)
            {
                yield return new WaitForSeconds(section.delayBeforeNext);
                section.audioObject.SetActive(true);
                yield return new WaitForSeconds(section.duration);
                section.audioObject.SetActive(false);
            }
        }

        isPlaying = false;
    }

    IEnumerator DisableDialogueObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (var obj in dialogueObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}
