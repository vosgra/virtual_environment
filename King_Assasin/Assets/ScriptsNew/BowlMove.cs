using UnityEngine;
using UnityEngine.UI;

public class EnableAndMoveObject : MonoBehaviour
{
    public GameObject targetObject; // The bowl
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    public Button activationButton; // UI Button to trigger activation

    private int currentWaypointIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        if (activationButton != null)
        {
            activationButton.onClick.AddListener(ActivateAndMove);
        }
    }

    void ActivateAndMove()
    {
        targetObject.SetActive(true);
        StartMovement();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveBetweenWaypoints();
        }
    }

    void StartMovement()
    {
        if (waypoints.Length > 0)
        {
            targetObject.transform.position = waypoints[0].position;
            currentWaypointIndex = 0;
            isMoving = true;
        }
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
}
