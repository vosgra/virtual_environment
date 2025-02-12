using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnableDisable : MonoBehaviour
{
    // The object the player will collide with (can be assigned in the Inspector).
    public GameObject player;

    // List of target GameObjects (first in the pair, the ones to collide with).
    public List<GameObject> targetGameObjects;

    // List of corresponding GameObjects to enable (second in the pair).
    public List<GameObject> objectsToControl;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is colliding with any of the target GameObjects
        for (int i = 0; i < targetGameObjects.Count; i++)
        {
            if (collision.gameObject == targetGameObjects[i])
            {
                // Enable the corresponding object in the list
                objectsToControl[i].SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the player stopped colliding with any of the target GameObjects
        for (int i = 0; i < targetGameObjects.Count; i++)
        {
            if (collision.gameObject == targetGameObjects[i])
            {
                // Disable the corresponding object in the list
                objectsToControl[i].SetActive(false);
            }
        }
    }
}
