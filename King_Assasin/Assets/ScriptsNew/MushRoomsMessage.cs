using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDisableHandler : MonoBehaviour
{
    [System.Serializable]
    public class EffectMapping
    {
        public GameObject targetObject; // Object to track for disable
        public GameObject effectObject; // Effect to enable when targetObject is disabled
    }

    public List<EffectMapping> effectMappings; // List of mappings for target objects and their effects

    private void Update()
    {
        foreach (EffectMapping mapping in effectMappings)
        {
            if (mapping.targetObject != null && !mapping.targetObject.activeInHierarchy && mapping.effectObject != null)
            {
                // Activate the effect
                mapping.effectObject.SetActive(true);

                // Disable the effect after 3 seconds
                StartCoroutine(DisableEffectAfterDelay(mapping.effectObject, 3f));

                // Remove the mapping to avoid repeated activation
                effectMappings.Remove(mapping);
                break;
            }
        }
    }

    private IEnumerator DisableEffectAfterDelay(GameObject effectObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (effectObject != null)
        {
            effectObject.SetActive(false);
        }
    }
}
