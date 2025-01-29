using UnityEngine;

public class RaftWaveEffect : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Amplitude controls how big the waves are (rotation amount).")]
    public float waveAmplitude = 5f; // Maximum rotation in degrees

    [Tooltip("Frequency controls how fast the waves are (speed of the effect).")]
    public float waveFrequency = 1f; // Speed of the wave effect

    [Tooltip("Smooth factor for transitions between waves.")]
    public float waveSmoothing = 0.5f;

    private float waveTimer = 0f;

    void Update()
    {
        // Calculate wave effect over time
        waveTimer += Time.deltaTime * waveFrequency;

        // Smooth sinusoidal wave rotation
        float waveRotation = Mathf.Sin(waveTimer) * waveAmplitude;

        // Smooth rotation around the Z-axis to simulate side-to-side motion
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, waveRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, waveSmoothing * Time.deltaTime);
    }
}
