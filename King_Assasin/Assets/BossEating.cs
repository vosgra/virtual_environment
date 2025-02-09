using System.Collections;
using UnityEngine;

public class BossSitting : MonoBehaviour
{
    private Animator animator;
    private bool isPerformingAction = false;

    public GameObject BowlMovement;
    public Transform Waypoint;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    [Header("Movement Settings")]
    public float moveDuration = 2f;
    public float stayDuration = 3f;
    public float returnDuration = 2f;

    [Header("Dying Movement Settings")]
    public Transform DyingWaypoint;
    public Vector3 dyingRotation;
    public float dyingMoveDuration = 2f;
    public int sittingTime;
    private Vector3 npcStartingPosition;
    private Quaternion npcStartingRotation;

    [Header("Trigger Settings")]
    public GameObject triggerObject;
    public float drinkingStartDelay = 3f;
    public GameObject checkObject;

    void OnEnable()
    {
        if (!animator) animator = GetComponent<Animator>();
        startingPosition = BowlMovement.transform.position;
        startingRotation = BowlMovement.transform.rotation;

        npcStartingPosition = transform.position;
        npcStartingRotation = transform.rotation;

        StartCoroutine(AnimationSequence());
    }

    private IEnumerator AnimationSequence()
    {
        if (isPerformingAction) yield break;
        isPerformingAction = true;

        animator.SetBool("IsSitting", true);
        yield return new WaitForSeconds(sittingTime);

        if (triggerObject != null)
        {
            while (!triggerObject.activeInHierarchy)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(drinkingStartDelay);

        animator.SetBool("IsSitting", false);
        animator.SetBool("IsDrinking", true);
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(MoveBowlSequence());
        yield return new WaitForSeconds(3f);

        if (checkObject != null && checkObject.activeInHierarchy)
        {
            animator.SetTrigger("IsDying");
            animator.SetBool("IsDrinking", false);
            StartCoroutine(MoveDuringDying());
        }
        else
        {
            animator.SetBool("IsDrinking", false);
            animator.SetBool("IsSitting", true);
        }

        isPerformingAction = false;
    }

    private IEnumerator MoveBowlSequence()
    {
        Quaternion targetRotation = Quaternion.Euler(-45f, -90f, 90f);
        yield return MoveToPositionAndRotation(BowlMovement, Waypoint.position, targetRotation, moveDuration);
        yield return new WaitForSeconds(stayDuration);
        yield return MoveToPositionAndRotation(BowlMovement, startingPosition, startingRotation, returnDuration);
    }

    private IEnumerator MoveDuringDying()
    {
        if (DyingWaypoint != null)
        {
            Quaternion targetRotation = Quaternion.Euler(dyingRotation);
            yield return MoveToPositionAndRotation(gameObject, DyingWaypoint.position, targetRotation, dyingMoveDuration);
        }
    }

    private IEnumerator MoveToPositionAndRotation(GameObject obj, Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        Vector3 startPosition = obj.transform.position;
        Quaternion startRotation = obj.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            obj.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
        obj.transform.rotation = targetRotation;
    }

    void OnDisable()
    {
        if (animator)
        {
            animator.SetBool("IsSitting", false);
            animator.SetBool("IsDrinking", false);
            animator.ResetTrigger("IsDying");
        }
        StopAllCoroutines();
        isPerformingAction = false;

        BowlMovement.transform.position = startingPosition;
        BowlMovement.transform.rotation = startingRotation;

        transform.position = npcStartingPosition;
        transform.rotation = npcStartingRotation;
    }
}
