using UnityEngine;
using UnityEngine.InputSystem;

public class DisableScriptWhileActive : MonoBehaviour
{
    [SerializeField] private GameObject[] specificGameObjects; // The GameObjects to check if enabled
    [SerializeField] private GameObject targetGameObject; // The GameObject whose script will be disabled
    [SerializeField] private PlayerInput scriptToDisable;
    private bool wasDisabledByScript = false;

    private void Start()
    {
        CheckAndToggleScript();
    }

    private void Update()
    {
        CheckAndToggleScript();
    }

    private void CheckAndToggleScript()
    {
        if (targetGameObject != null && scriptToDisable != null)
        {
            bool shouldDisable = false;
            foreach (GameObject obj in specificGameObjects)
            {
                if (obj != null && obj.activeInHierarchy)
                {
                    shouldDisable = true;
                    break;
                }
            }

            if (shouldDisable && scriptToDisable.enabled)
            {
                scriptToDisable.enabled = false;
                wasDisabledByScript = true;
            }
            else if (!shouldDisable && wasDisabledByScript)
            {
                scriptToDisable.enabled = true;
                wasDisabledByScript = false;
            }
        }
    }
}
