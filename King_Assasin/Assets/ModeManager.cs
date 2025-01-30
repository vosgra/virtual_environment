using UnityEngine;
using System.Collections.Generic;

public class ScriptToggleManager : MonoBehaviour
{
    public GameObject objectA; // First object
    public GameObject objectB; // Second object

    public List<MonoBehaviour> enableIfA; // Scripts to enable if A is active
    public List<MonoBehaviour> disableIfA; // Scripts to disable if A is active

    public List<MonoBehaviour> enableIfB; // Scripts to enable if B is active
    public List<MonoBehaviour> disableIfB; // Scripts to disable if B is active

    void Update()
    {
        if (objectA.activeSelf && !objectB.activeSelf)
        {
            ToggleScripts(enableIfA, true);
            ToggleScripts(disableIfA, false);
        }
        else if (objectB.activeSelf && !objectA.activeSelf)
        {
            ToggleScripts(enableIfB, true);
            ToggleScripts(disableIfB, false);
        }
    }

    private void ToggleScripts(List<MonoBehaviour> scripts, bool enable)
    {
        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = enable;
            }
        }
    }
}
