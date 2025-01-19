using System.Collections;
using UnityEngine;

public class NPCShopScene : MonoBehaviour
{
    public enum NPCState { IsSitting, IsTalking }
    public NPCState currentState = NPCState.IsSitting;

    public Animator animator;
    public float interactionDistance = 2f;
    public KeyCode interactKey = KeyCode.E;

    private Transform player;
    private bool isPlayerInRange = false;

    public GameObject objectToTeleport; // The object to teleport
    public Transform teleportTarget;   // The target position to teleport to

    private bool movementDisabled = false; // To track if movement is disabled

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set the default state to sitting
        SetState(NPCState.IsSitting);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(DelayedTransitionToTalking());
            TeleportObject();
            StartCoroutine(DisableMovementForDuration(16f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    IEnumerator DelayedTransitionToTalking()
    {
        // Wait for 8 seconds before transitioning to Talking state
        yield return new WaitForSeconds(8f);

        // Start Talking State
        SetState(NPCState.IsTalking);

        // Wait for 4 seconds in Talking state
        yield return new WaitForSeconds(4f);

        // Return to Sitting State
        SetState(NPCState.IsSitting);
    }

    void TeleportObject()
    {
        if (objectToTeleport != null && teleportTarget != null)
        {
            objectToTeleport.transform.position = teleportTarget.position;
        }
    }

    IEnumerator DisableMovementForDuration(float duration)
    {
        movementDisabled = true;
        yield return new WaitForSeconds(duration);
        movementDisabled = false;
    }

    

    

    void SetState(NPCState newState)
    {
        currentState = newState;

        animator.SetBool("IsSitting", currentState == NPCState.IsSitting);
        animator.SetBool("IsIdle", currentState == NPCState.IsTalking);
    }
}
