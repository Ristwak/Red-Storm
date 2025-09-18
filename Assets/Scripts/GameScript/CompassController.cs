using UnityEngine;

public class CompassController : MonoBehaviour
{
    [Header("References")]
    public Transform player;        // XR Rig root (the body, not just the camera)
    public Transform targetHouse;   // The target (base / house)
    public Transform needle;        // The needle object in compass

    [Header("Settings")]
    public float rotationSpeed = 180f; // Degrees per second
    [Tooltip("Offset in degrees to correct needle alignment")]
    public float rotationOffset = 0f;  // Manual offset for correction

    void Update()
    {
        if (player == null || targetHouse == null || needle == null) return;

        // Get direction from player to target (ignore height difference)
        Vector3 toTarget = targetHouse.position - player.position;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude < 0.01f) return;

        // World-space forward direction of player (ignoring tilt)
        Vector3 playerForward = player.forward;
        playerForward.y = 0;

        // Angle between player forward and target direction
        float angleToTarget = Vector3.SignedAngle(playerForward, toTarget, Vector3.up);

        // Apply offset
        float finalAngle = -angleToTarget + rotationOffset;

        // Desired rotation
        Quaternion desiredRotation = Quaternion.Euler(0, finalAngle, 0);

        // Debugging
        Debug.Log($"AngleToTarget: {angleToTarget}, FinalAngle (with offset): {finalAngle}");

        // Smoothly rotate the needle
        needle.localRotation = Quaternion.RotateTowards(
            needle.localRotation,
            desiredRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
