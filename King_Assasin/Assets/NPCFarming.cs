using UnityEngine;
using System.Collections;

public class NPCBehavior : MonoBehaviour
{
    // Reference to the Animator component of the NPC
    public Animator npcAnimator;

    // Walking variables
    public float walkDistance = 5f; // Distance the NPC will walk before planting
    public float walkSpeed = 2f; // Speed at which the NPC walks

    // Animator boolean states
    private string isWalkingBool = "IsWalking";
    private string isPlantingBool = "IsPlanting";
    private string isWateringBool = "IsWatering";

    private Vector3 startPoint; // Starting position of the NPC
    private Vector3 targetPoint; // Target position for walking
    private bool isWalking = true;

    void Start()
    {
        // Initialize start and target points
        startPoint = transform.position;
        targetPoint = startPoint + transform.forward * walkDistance;

        // Set initial walking state
        npcAnimator.SetBool(isWalkingBool, true);
    }

    void Update()
    {
        if (isWalking)
        {
            Walk();
        }
    }

    void Walk()
    {
        // Move the NPC towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, walkSpeed * Time.deltaTime);

        // Check if the NPC has reached the target point
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            isWalking = false;
            npcAnimator.SetBool(isWalkingBool, false);
            StartCoroutine(PerformActions());
        }
    }

    IEnumerator PerformActions()
    {
        // Play planting animation using boolean
        npcAnimator.SetBool(isPlantingBool, true);
        yield return new WaitForSeconds(1f); // Allow time for transition to start
        yield return new WaitForSeconds(npcAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Stop planting animation
        npcAnimator.SetBool(isPlantingBool, false);

        // Play watering animation using boolean
        npcAnimator.SetBool(isWateringBool, true);
        yield return new WaitForSeconds(1f); // Allow time for transition to start
        yield return new WaitForSeconds(npcAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Stop watering animation
        npcAnimator.SetBool(isWateringBool, false);

        // Update start and target points for the next walk cycle
        startPoint = targetPoint;
        targetPoint = startPoint + transform.forward * walkDistance;

        // Resume walking
        isWalking = true;
        npcAnimator.SetBool(isWalkingBool, true);
    }
}
