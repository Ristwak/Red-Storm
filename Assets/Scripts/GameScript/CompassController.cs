using UnityEngine;

public class CompassController : MonoBehaviour
{
    [Header("References")]
    public Transform player;       // XR Rig or player root
    public Transform targetHouse;  // The house/destination
    public Transform needle;       // The white needle object

    [Header("Settings")]
    public float rotationSpeed = 180f; // Degrees per second for smooth rotation

    void Update()
    {
        if (player == null || targetHouse == null || needle == null) return;

        // Direction from player to target (ignore Y)
        Vector3 direction = targetHouse.position - player.position;
        direction.y = 0;

        if (direction.sqrMagnitude < 0.01f) return; // Prevent errors if super close

        // Get the angle to the target relative to world forward
        float angleToTarget = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

        // Get player's yaw
        float playerYaw = player.eulerAngles.y;

        // Compass angle relative to player forward
        float compassAngle = angleToTarget - playerYaw;

        // Desired rotation for needle (around Z-axis in local space)
        Quaternion targetRotation = Quaternion.Euler(0, 0, -compassAngle);

        // Smoothly rotate the needle
        needle.localRotation = Quaternion.RotateTowards(
            needle.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
