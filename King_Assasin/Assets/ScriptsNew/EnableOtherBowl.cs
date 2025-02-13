using UnityEngine;

public class ActivateBasedOnState : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public GameObject outputA;
    public GameObject outputB;

    void OnEnable()
    {
        // Check which object is active and enable the corresponding output
        if (objectA.activeSelf)
        {
            outputA.SetActive(true);
            outputB.SetActive(false);
        }
        else if (objectB.activeSelf)
        {
            outputA.SetActive(false);
            outputB.SetActive(true);
        }
        else
        {
            // If neither object is active, disable both outputs
            outputA.SetActive(false);
            outputB.SetActive(false);
        }
    }
}