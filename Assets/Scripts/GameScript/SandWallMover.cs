using UnityEngine;

public class SandWallMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform target;      // usually the XR Rig (player)
    public float minSpeed = 5f;   // minimum storm speed
    public float maxSpeed = 15f;  // maximum storm speed
    public float changeRate = 2f; // how fast speed changes

    private float currentSpeed;

    void Start()
    {
        // start with a random speed between min and max
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (target == null) return;

        // Smoothly vary the speed up and down
        float noise = Mathf.PerlinNoise(Time.time / changeRate, 0f); 
        currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, noise);

        // Move storm wall towards the target
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(target.position.x, transform.position.y, target.position.z),
            currentSpeed * Time.deltaTime
        );
    }
}
