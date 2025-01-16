using UnityEngine;
using UnityEngine.Playables;

public class CameraWaypointMover : MonoBehaviour
{
    [Header("Camera Waypoints and Rotation")]
    public Waypoint[] waypoints;   // List of waypoints for the camera to follow.
    private int currentWaypointIndex = 0;  // Index of the current waypoint.
    private Transform currentWaypoint;  // Reference to the current waypoint.

    [Header("Speed Settings")]
    public float movementSpeed = 1.0f; // Speed of camera movement towards waypoints.
    public float rotationSpeed = 1.0f; // Speed of the camera rotation transition.

    private float journeyLength;  // The distance between the start and end waypoints.
    private float startTime;  // Time when the camera started moving to the current waypoint.

    private Quaternion startRotation;  // Starting rotation of the camera.
    private Quaternion targetRotation; // Target rotation to reach.

    [Header("Timeline Control")]
    public PlayableDirector playableDirector; // PlayableDirector to control the timeline.
    public bool isCutscenePlaying = false; // Whether the cutscene is currently playing.

    void Start()
    {
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0].position;  // Start at the first waypoint.
            transform.position = currentWaypoint.position; // Position the camera at the start.
            startRotation = transform.rotation;  // Set the start rotation for smooth transition.
        }
    }

    void Update()
    {
        // Play the Timeline if it's not already playing.
        if (!isCutscenePlaying && playableDirector != null)
        {
            playableDirector.Play();
            isCutscenePlaying = true; // Mark cutscene as playing.
        }

        // Only update the camera position and rotation when the timeline is playing.
        if (isCutscenePlaying)
        {
            MoveCameraThroughWaypoints();
        }
    }

    void MoveCameraThroughWaypoints()
    {
        if (currentWaypointIndex >= waypoints.Length) return; // Stop if no more waypoints.

        Waypoint currentWaypoint = waypoints[currentWaypointIndex];
        Waypoint nextWaypoint = (currentWaypointIndex + 1 < waypoints.Length) ? waypoints[currentWaypointIndex + 1] : null;

        if (nextWaypoint != null)
        {
            // Calculate the total distance and time to travel
            journeyLength = Vector3.Distance(transform.position, nextWaypoint.position.position);
            startTime = Time.time;

            // Move towards the next waypoint.
            float distanceCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Interpolate between positions.
            transform.position = Vector3.Lerp(transform.position, nextWaypoint.position.position, fractionOfJourney);

            // Smoothly interpolate the rotation from the start to the target rotation.
            targetRotation = Quaternion.Euler(nextWaypoint.xRotation, nextWaypoint.yRotation, 0);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, fractionOfJourney);

            // Check if the camera reached the waypoint.
            if (fractionOfJourney >= 1f)
            {
                // After reaching the waypoint, set the next waypoint
                currentWaypointIndex++;
                startRotation = transform.rotation; // Reset the start rotation to the new position.
                startTime = Time.time; // Reset the start time to ensure the next move is smooth.
            }
        }
    }
}

[System.Serializable]
public class Waypoint
{
    public Transform position;  // Position of the waypoint (can be set to an empty GameObject).
    public float xRotation;     // Desired X-axis rotation, adjustable in the inspector.
    public float yRotation;     // Desired Y-axis rotation, adjustable in the inspector.
}
