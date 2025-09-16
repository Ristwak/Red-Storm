using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class XRGravity : MonoBehaviour
{
    public float gravity = -9.81f;   // Earth gravity
    public float groundedOffset = 0.1f; // small offset to detect ground
    private CharacterController controller;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if grounded
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 
            controller.height / 2 + groundedOffset);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // keep grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // apply gravity
        }

        // Apply movement (only Y, since XZ comes from XR movement)
        Vector3 move = new Vector3(0, verticalVelocity, 0);
        controller.Move(move * Time.deltaTime);
    }
}
