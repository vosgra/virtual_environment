using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    public enum NPCState { IsLaying, IsTalking, IsWalking, IsPointing }
    public NPCState currentState = NPCState.IsLaying;

    public Transform player;
    public float interactionDistance = 2f;
    public KeyCode interactKey = KeyCode.E;

    public Transform[] walkRoute;
    public Animator animator;
    public NavMeshAgent navAgent;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        SetState(NPCState.IsLaying);
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCState.IsLaying:
                HandleLayingState();
                break;
            case NPCState.IsTalking:
                HandleTalkingState();
                break;
            case NPCState.IsWalking:
                HandleWalkingState();
                break;
            case NPCState.IsPointing:
                // Pointing handled in coroutine
                break;
        }
    }

    void HandleLayingState()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionDistance && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(TalkingState());
        }
    }

    void HandleTalkingState()
    {
        // Rotate to face the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    IEnumerator TalkingState()
    {
        SetState(NPCState.IsTalking);

        yield return new WaitForSeconds(5f);

        StartCoroutine(WalkingState());
    }

    IEnumerator WalkingState()
    {
        SetState(NPCState.IsWalking);

        foreach (Transform waypoint in walkRoute)
        {
            navAgent.SetDestination(waypoint.position);
            while (Vector3.Distance(transform.position, waypoint.position) > navAgent.stoppingDistance)
            {
                yield return null;
            }
        }

        StartCoroutine(PointingState());
    }

    IEnumerator PointingState()
    {
        SetState(NPCState.IsPointing);

        yield return new WaitForSeconds(4f);

        StartCoroutine(ReturnToLayingState());
    }

    IEnumerator ReturnToLayingState()
    {
        SetState(NPCState.IsWalking);

        navAgent.SetDestination(startPosition);
        while (Vector3.Distance(transform.position, startPosition) > navAgent.stoppingDistance)
        {
            yield return null;
        }

        transform.rotation = startRotation;
        SetState(NPCState.IsLaying);
    }

    void HandleWalkingState()
    {
        animator.SetBool("IsWalking", true);
    }

    void SetState(NPCState newState)
    {
        currentState = newState;

        animator.SetBool("IsLaying", currentState == NPCState.IsLaying);
        animator.SetBool("IsTalking", currentState == NPCState.IsTalking);
        animator.SetBool("IsWalking", currentState == NPCState.IsWalking);
        animator.SetBool("IsPointing", currentState == NPCState.IsPointing);

        if (currentState != NPCState.IsWalking)
        {
            navAgent.ResetPath();
        }
    }
}
