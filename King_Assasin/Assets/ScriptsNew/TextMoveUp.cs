using UnityEngine;

public class MoveBetweenWaypoints : MonoBehaviour
{
    public GameObject objectToMove;  // The GameObject that will be moved
    public Transform waypoint1;      // First waypoint
    public Transform waypoint2;      // Second waypoint
    public float speed = 2f;         // Speed of the movement

    private Transform currentTarget;  // Current target to move towards

    void Start()
    {
        // If no object is assigned, use the GameObject this script is attached to
        if (objectToMove == null)
        {
            objectToMove = this.gameObject;
        }

        // Teleport the object to the first waypoint at the start
        objectToMove.transform.position = waypoint1.position;

        // Start by moving to the first waypoint
        currentTarget = waypoint1;
    }

    void Update()
    {
        // Check if the object is not at the current target's position
        if (Vector3.Distance(objectToMove.transform.position, currentTarget.position) > 0.1f)
        {
            // Move towards the current target
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget.position, speed * Time.deltaTime);
        }
        else
        {
            // Once the object reaches the current target, switch the target
            currentTarget = currentTarget == waypoint1 ? waypoint2 : waypoint1;
        }
    }
}
