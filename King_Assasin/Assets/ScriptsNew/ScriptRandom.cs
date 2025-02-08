using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCDialogueTrigger : MonoBehaviour
{
    public GameObject[] dialogues; // Assign dialogue UI panels in Inspector
    public GameObject[] voiceObjects; // Assign corresponding AudioSource GameObjects
    public float typingSpeed = 0.05f;
    public GameObject effectObject;
    public float effectDuration = 3f;

    private bool playerInRange = false;
    private bool hasTriggered = false;
    private GameObject currentVoiceObject;

    void Update()
    {
        if (playerInRange && !hasTriggered && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShowRandomDialogue());
            hasTriggered = true;
        }
    }

    IEnumerator ShowRandomDialogue()
    {
        if (dialogues.Length == 0) yield break;

        // Disable all dialogues and voice objects first
        foreach (GameObject dialogue in dialogues)
        {
            dialogue.SetActive(false);
        }
        foreach (GameObject voice in voiceObjects)
        {
            if (voice != null) voice.SetActive(false);
        }

        int randomIndex = Random.Range(0, dialogues.Length);
        GameObject selectedDialogue = dialogues[randomIndex];
        selectedDialogue.SetActive(true);

        // Enable corresponding voice object
        if (voiceObjects.Length > randomIndex && voiceObjects[randomIndex] != null)
        {
            currentVoiceObject = voiceObjects[randomIndex];
            currentVoiceObject.SetActive(true);
        }

        if (effectObject != null)
        {
            effectObject.SetActive(true);
            StartCoroutine(DisableEffectAfterDelay(effectDuration));
        }

        Text dialogueText = selectedDialogue.GetComponentInChildren<Text>();
        if (dialogueText == null) yield break;

        string fullText = dialogueText.text;
        dialogueText.text = "";

        // Typewriter effect
        foreach (char letter in fullText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait before hiding
        yield return new WaitForSeconds(5f);

        selectedDialogue.SetActive(false);
        if (currentVoiceObject != null)
        {
            currentVoiceObject.SetActive(false);
        }

        hasTriggered = false;
    }

    IEnumerator DisableEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (effectObject != null)
        {
            effectObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}