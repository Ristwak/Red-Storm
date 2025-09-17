using UnityEngine;

public class SandWallMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform target;        // XR Rig (player)
    public float minSpeed = 5f;     // minimum storm speed
    public float maxSpeed = 15f;    // maximum storm speed
    public float changeRate = 2f;   // how fast speed changes

    private float currentSpeed;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("SandWallMover: No target assigned!");
            enabled = false;
            return;
        }

        // Start with a random speed
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (target == null) return;

        // Smoothly vary speed using Perlin noise
        float noise = Mathf.PerlinNoise(Time.time / changeRate, 0f);
        currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, noise);

        // Keep Y constant so storm only follows horizontally
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);

        // Always move storm toward player
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            currentSpeed * Time.deltaTime
        );
    }
}
