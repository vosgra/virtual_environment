using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic; // Required for List

public class CostumerBehavior : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Animator npcAnimator;

    private string isWalkingBool = "IsWalking";
    private string isIdleBool = "IsIdle";
    private string isTalkingBool = "IsTalking";

    public List<Transform> waypoints = new List<Transform>(); // List of waypoints

    public float idleDuration = 2f;   // Idle duration (modifiable in Inspector)
    public float talkingDuration = 2f; // Talking duration (modifiable in Inspector)

    private int targetIndex = 0; // To track the current target index
    private bool isAnimating = false; // To prevent overlapping animations

    private bool isPlayerNearby = false; // To track if the player is nearby
    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            return;
        }

        // Check if waypoints are assigned
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned! Add waypoints to the list in the Inspector.");
            return;
        }

        // Start the movement to the first target
        MoveToNextTarget();
    }

    void Update()
    {
        // Update walking animation based on velocity
        if (navMeshAgent.velocity.magnitude > 0.1f && !isAnimating) // If the NPC is moving
        {
            npcAnimator.SetBool(isWalkingBool, true);
            npcAnimator.SetBool(isIdleBool, false);
        }
        else if (!isAnimating) // If the NPC is idle
        {
            npcAnimator.SetBool(isWalkingBool, false);
            npcAnimator.SetBool(isIdleBool, true);
        }

        // Check if the NPC has reached the current target
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!isAnimating)
            {
                StartCoroutine(IdleAtStop());
            }
        }

        // Handle interaction with the player
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TalkToPlayer());
        }

        // Rotate towards the player only when talking
        if (npcAnimator.GetBool(isTalkingBool) && playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }

    // Move the NPC to the next target in the sequence
    void MoveToNextTarget()
    {
        if (waypoints.Count == 0)
            return;

        navMeshAgent.SetDestination(waypoints[targetIndex].position);

        // Determine the next target index
        targetIndex = (targetIndex + 1) % waypoints.Count;
    }

    // Idle at specific waypoints
    IEnumerator IdleAtStop()
    {
        isAnimating = true;

        // Stop the NavMeshAgent
        navMeshAgent.isStopped = true;

        // Play idle animation
        npcAnimator.SetBool(isIdleBool, true);
        npcAnimator.SetBool(isWalkingBool, false);
        yield return new WaitForSeconds(idleDuration); // Idle for specified duration

        // Resume movement
        navMeshAgent.isStopped = false;
        MoveToNextTarget();

        isAnimating = false;
    }

    // Handle talking to the player
    IEnumerator TalkToPlayer()
    {
        isAnimating = true;

        // Stop movement and animations
        navMeshAgent.isStopped = true;
        npcAnimator.SetBool(isWalkingBool, false);
        npcAnimator.SetBool(isIdleBool, false);

        // Play talking animation
        npcAnimator.SetBool(isTalkingBool, true);
        yield return new WaitForSeconds(talkingDuration); // Talking duration

        // Stop talking animation
        npcAnimator.SetBool(isTalkingBool, false);

        // Resume movement
        navMeshAgent.isStopped = false;

        isAnimating = false;
    }

    // Detect when the player enters the NPC's collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerTransform = other.transform;
        }
    }

    // Detect when the player exits the NPC's collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerTransform = null;
        }
    }
}
