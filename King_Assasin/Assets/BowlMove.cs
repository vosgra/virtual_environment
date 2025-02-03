using UnityEngine;

public class EnableAndMoveObject : MonoBehaviour
{
    public GameObject targetObject;
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    public GameObject requiredObject;  // Reference to the GameObject that needs to be activated first

    private bool playerInRange = false;
    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private bool hasRequiredObjectBeenActivated = false;
    private bool hasBeenActivated = false;  // Track if the object has been activated already

    void Update()
    {
        // Check if the required object has ever been activated
        if (requiredObject != null && requiredObject.activeInHierarchy)
        {
            hasRequiredObjectBeenActivated = true;
        }

        // Only allow activation if requirements are met and it hasn't been activated before
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && hasRequiredObjectBeenActivated && !hasBeenActivated)
        {
            targetObject.SetActive(true);
            StartMovement();
            hasBeenActivated = true;  // Ensure it can only be activated once
        }

        if (isMoving)
        {
            MoveBetweenWaypoints();
        }
    }

    void StartMovement()
    {
        currentWaypointIndex = 0;
        isMoving = true;
        targetObject.transform.position = waypoints[0].position;
    }

    void MoveBetweenWaypoints()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(targetObject.transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    isMoving = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}