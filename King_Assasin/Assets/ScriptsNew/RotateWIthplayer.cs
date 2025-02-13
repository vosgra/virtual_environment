using UnityEngine;

public class RotationFollower : MonoBehaviour
{
    [SerializeField] private Transform referenceObject; // Object to track
    [SerializeField] private float rotationMultiplier = 1f; // Strength of effect
    [SerializeField] private float rotationSpeed = 5f; // Speed of smooth rotation

    private void Update()
    {
        if (referenceObject == null) return;

        // Get the Y rotation of the reference object
        float targetYRotation = referenceObject.eulerAngles.y;

        // Ensure correct rotation mapping by using Mathf.DeltaAngle
        float adjustedYRotation = Mathf.DeltaAngle(0, targetYRotation);

        // Apply correct Z rotation (fixing top-bottom reversal)
        float targetZRotation = adjustedYRotation * rotationMultiplier;

        // Get the current rotation
        Vector3 currentRotation = transform.eulerAngles;

        // Smoothly interpolate the Z rotation
        float smoothedZRotation = Mathf.LerpAngle(currentRotation.z, targetZRotation, Time.deltaTime * rotationSpeed);

        // Apply the new rotation
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, smoothedZRotation);
    }
}
