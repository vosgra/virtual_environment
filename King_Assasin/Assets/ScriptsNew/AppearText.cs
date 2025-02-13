using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float characterDelay = 0.05f; // Delay between characters

    private string fullText;

    void Start()
    {
        // Store the full text and hide it at the start
        fullText = textComponent.text;
        textComponent.text = "";

        // Start the typewriter effect
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            yield return new WaitForSeconds(characterDelay);
        }
    }
}
