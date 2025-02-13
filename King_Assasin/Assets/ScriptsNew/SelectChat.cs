using UnityEngine;

public class ScriptEnabler : MonoBehaviour
{
    public GameObject firstObject;
    public GameObject secondObject;

    public MonoBehaviour firstScript;
    public MonoBehaviour secondScript;
    public MonoBehaviour fallbackScript;

    void Update()
    {
        if (firstObject.activeInHierarchy)
        {
            EnableOnly(firstScript);
        }
        else if (secondObject.activeInHierarchy)
        {
            EnableOnly(secondScript);
        }
        else
        {
            EnableOnly(fallbackScript);
        }
    }

    void EnableOnly(MonoBehaviour scriptToEnable)
    {
        firstScript.enabled = (scriptToEnable == firstScript);
        secondScript.enabled = (scriptToEnable == secondScript);
        fallbackScript.enabled = (scriptToEnable == fallbackScript);
    }
}
