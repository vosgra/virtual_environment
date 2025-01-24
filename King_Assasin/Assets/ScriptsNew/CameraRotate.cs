using UnityEngine;

public class RotateCameras : MonoBehaviour
{
    public Transform npc; // Reference to the NPC
    public Transform camera1; // First camera
    public Transform camera2; // Second camera
    public float rotationSpeed = 10f; // Speed of rotation
    public float radius = 5f; // Distance from the NPC
    public float heightOffset = 2f; // Height above the NPC

    private float angle = 0f; // Current angle for rotation

    void Update()
    {
        if (npc == null || camera1 == null || camera2 == null) return;

        // Increment the angle based on rotation speed
        angle += rotationSpeed * Time.deltaTime;
        if (angle >= 360f) angle -= 360f;

        // Update camera positions
        PositionCamera(camera1, angle, true); // Pass true for camera1 to adjust focus
        PositionCamera(camera2, angle + 150f, false); // Camera2 is opposite to Camera1
    }

    void PositionCamera(Transform camera, float angleDegrees, bool isCamera1)
    {
        // Convert angle to radians for Mathf calculations
        float angleRadians = angleDegrees * Mathf.Deg2Rad;

        // Calculate position relative to the NPC
        Vector3 offset = new Vector3(
            Mathf.Sin(angleRadians) * radius, // X position (circular motion)
            heightOffset,                     // Height above the NPC
            Mathf.Cos(angleRadians) * radius  // Z position (circular motion)
        );

        camera.position = npc.position + offset;

        // Adjust focus for camera1 to aim higher at NPC's head
        if (isCamera1)
        {
            Vector3 npcHeadPosition = npc.position + Vector3.up * 1.5f; // Adjust the "1.5f" value as needed for the NPC's head height
            camera.LookAt(npcHeadPosition);
        }
        else
        {
            camera.LookAt(npc);
        }
    }
}
