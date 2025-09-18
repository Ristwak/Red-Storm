using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SandWallMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform target;        // XR Rig (player root)
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

        // Make sure collider is set as trigger
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
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

    private void OnTriggerEnter(Collider other)
    {
        // ✅ Check if the collided object has a CharacterController in itself or parents
        CharacterController cc = other.GetComponentInChildren<CharacterController>();
        if (cc != null)
        {
            Debug.Log("🌪 Storm caught the player (CharacterController)!");
            FindObjectOfType<GameManager>().PlayerHitByStorm();
        }
        else
        {
            Debug.Log("Storm collided with: " + other.name);
        }
    }
}
