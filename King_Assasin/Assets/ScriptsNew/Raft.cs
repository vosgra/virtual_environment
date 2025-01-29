using UnityEngine;

public class RaftMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the raft to follow
    public float speed = 5f; // Speed at which the raft moves
    public GameObject objectToDisable; // The game object to disable when the raft reaches the final waypoint
    public MonoBehaviour scriptToDisable; // The script to disable when the raft reaches the final waypoint

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // Move the raft towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            // Check if the raft has reached the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++; // Move to the next waypoint

                // Check if the raft has reached the final waypoint
                if (currentWaypointIndex >= waypoints.Length)
                {
                    OnFinalWaypointReached();
                }
            }
        }
    }

    void OnFinalWaypointReached()
    {
        Debug.Log("Final waypoint reached!");

        // Disable the specified game object
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }

        // Disable the specified script
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
        }
    }
}