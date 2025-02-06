using UnityEngine;
using UnityEngine.AI;

public class NPCWaypointWalker : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform specialWaypoint;
    public Transform teleportWaypoint;
    public GameObject specialObject;
    public float stoppingDistance = 0.5f;
    public float specialStoppingDistance = 2.0f;
    public GameObject objectToEnable; // Object to enable after death sequence
    public GameObject objectToDisableOnArrival; // New field for object to disable on arrival
    public GameObject objectToEnableOnArrival; // New field for object to enable on arrival

    private NavMeshAgent agent;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private bool specialSequenceTriggered = false;
    private bool specialActionsPerformed = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (waypoints.Length > 0)
        {
            MoveToNextWaypoint();
        }
    }

    void Update()
    {
        if (specialSequenceTriggered && !specialActionsPerformed &&
            Vector3.Distance(transform.position, specialWaypoint.position) <= specialStoppingDistance)
        {
            PerformSpecialActions();
        }
        else if (agent.remainingDistance <= stoppingDistance && !agent.pathPending && !specialSequenceTriggered)
        {
            if (specialObject.activeSelf && currentWaypointIndex == 0)
            {
                TriggerSpecialSequence();
            }
            else
            {
                MoveToNextWaypoint();
            }
        }

        animator.SetBool("IsWalking", agent.velocity.magnitude > 0.1f);
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void TriggerSpecialSequence()
    {
        specialSequenceTriggered = true;
        agent.destination = specialWaypoint.position;
    }

    void PerformSpecialActions()
    {
        specialActionsPerformed = true;

        // Teleport to the designated waypoint
        transform.position = teleportWaypoint.position;

        // Disable and enable specified objects upon arrival
        if (objectToDisableOnArrival != null)
            objectToDisableOnArrival.SetActive(false);
        if (objectToEnableOnArrival != null)
            objectToEnableOnArrival.SetActive(true);

        // Perform IsSitting animation
        animator.SetBool("IsWalking", false);
        animator.SetTrigger("IsSitting");

        // Start coroutine for sequential animations
        StartCoroutine(SpecialAnimationSequence());
    }

    System.Collections.IEnumerator SpecialAnimationSequence()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("IsDrinking");

        // Wait until IsDrinking animation ends
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("IsDrinking") &&
                                      animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        animator.SetTrigger("IsDying");

        // Wait for IsDying animation to finish
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("IsDying") &&
                                      animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        // Disable NPC and enable final object
        gameObject.SetActive(false);
        if (objectToEnable != null)
            objectToEnable.SetActive(true);
    }
}