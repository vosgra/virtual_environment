using UnityEngine;

public class ScenarioSwitcher : MonoBehaviour
{
    [Header("Scenario Triggers")]
    public GameObject scenario1Trigger;
    public GameObject scenario2Trigger;

    [Header("Scenario 1 Objects")]
    public GameObject[] scenario1Enable;
    public GameObject[] scenario1Disable;

    [Header("Scenario 2 Objects")]
    public GameObject[] scenario2Enable;
    public GameObject[] scenario2Disable;

    void Update()
    {
        if (scenario1Trigger.activeInHierarchy && !scenario2Trigger.activeInHierarchy)
        {
            ActivateScenario(scenario1Enable, scenario1Disable);
        }
        else if (scenario2Trigger.activeInHierarchy && !scenario1Trigger.activeInHierarchy)
        {
            ActivateScenario(scenario2Enable, scenario2Disable);
        }
    }

    void ActivateScenario(GameObject[] enableObjects, GameObject[] disableObjects)
    {
        foreach (GameObject obj in enableObjects)
        {
            if (obj) obj.SetActive(true);
        }

        foreach (GameObject obj in disableObjects)
        {
            if (obj) obj.SetActive(false);
        }
    }
}
