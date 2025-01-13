using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FarmerBehavior : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Animator npcAnimator;

    private string isWalkingBool = "IsWalking";
    private string isPlantingBool = "IsPlanting";
    private string isWateringBool = "IsWatering";

    public Transform cube1;   // The position of Cube#1
    public Transform cube2;   // The position of Cube#2
    public Transform cube3;   // The position of Cube#3
    public Transform cube4;   // The position of Cube#4
    public Transform cube5;   // The position of Cube#5
    public Transform cube6;   // The position of Cube#6

    private Transform[] targets; // Array to store target cubes
    private int targetIndex = 0; // To track the current target index
    private bool isAnimating = false; // To prevent overlapping animations

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            return;
        }

        // Set up the target cubes in the desired sequence
        targets = new Transform[] { cube1, cube2, cube3, cube4, cube5, cube6 };

        // Start the movement to the first target
        MoveToNextTarget();
    }

    void Update()
    {
        // Debugging: Track the NPC's movement and animation state
        Debug.Log($"Is Walking: {npcAnimator.GetBool(isWalkingBool)}, Position: {transform.position}");

        // Update walking animation based on velocity
        if (navMeshAgent.velocity.magnitude > 0.1f) // If the NPC is moving
        {
            npcAnimator.SetBool(isWalkingBool, true);
        }
        else // If the NPC is not moving
        {
            npcAnimator.SetBool(isWalkingBool, false);
        }

        // Check if the NPC has reached the current target
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Perform actions (planting and watering) when the NPC stops, if not already animating
            if (!isAnimating)
            {
                StartCoroutine(PerformActionsAtStop());
            }
        }
    }

    // Move the NPC to the next target in the sequence
    void MoveToNextTarget()
    {
        // Set the destination to the current target
        navMeshAgent.SetDestination(targets[targetIndex].position);

        // Log the target position for debugging
        Debug.Log($"Moving to target: {targets[targetIndex].name} at position: {targets[targetIndex].position}");
    }

    // Perform actions (planting and watering) at the current stop
    IEnumerator PerformActionsAtStop()
    {
        // Prevent overlapping actions
        if (isAnimating)
            yield break;

        isAnimating = true;

        // Stop movement while performing actions
        navMeshAgent.isStopped = true;

        // Ensure the walking animation stops when NPC is idle
        npcAnimator.SetBool(isWalkingBool, false);

        // Perform planting animation
        npcAnimator.SetBool(isPlantingBool, true);
        yield return new WaitForSeconds(1f); // Wait for planting animation to transition in
        yield return new WaitForSeconds(npcAnimator.GetCurrentAnimatorStateInfo(0).length); // Wait for animation to finish
        npcAnimator.SetBool(isPlantingBool, false);

        // Perform watering animation
        npcAnimator.SetBool(isWateringBool, true);
        yield return new WaitForSeconds(1f); // Wait for watering animation to transition in
        yield return new WaitForSeconds(npcAnimator.GetCurrentAnimatorStateInfo(0).length); // Wait for animation to finish
        npcAnimator.SetBool(isWateringBool, false);

        // Resume movement after performing actions
        navMeshAgent.isStopped = false;

        // Determine the next target index
        if (targetIndex == 5) // If we're at cube6, go backwards
        {
            targetIndex = 4;
        }
        else if (targetIndex == 0) // If we're at cube1, move forwards
        {
            targetIndex = 1;
        }
        else
        {
            targetIndex = (targetIndex == 1 || targetIndex == 2 || targetIndex == 3 || targetIndex == 4) ? targetIndex + 1 : targetIndex - 1;
        }

        // Move to the next target
        MoveToNextTarget();

        // Allow the sequence to continue
        isAnimating = false;
    }
}
