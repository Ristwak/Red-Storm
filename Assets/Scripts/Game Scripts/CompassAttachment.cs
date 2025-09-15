using UnityEngine;

public class CompassAttachment : MonoBehaviour
{
    [Header("Attach To Hand")]
    public Transform handTransform;   // Assign the hand (e.g. RightHand / Wrist)

    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;

    void Start()
    {
        if (handTransform != null)
        {
            // Make compass a child of the hand
            transform.SetParent(handTransform);

            // Save the current position and rotation as offsets
            initialLocalPos = transform.localPosition;
            initialLocalRot = transform.localRotation;
        }
        else
        {
            Debug.LogWarning("CompassAttachment: Hand Transform not assigned!");
        }
    }

    void LateUpdate()
    {
        if (handTransform != null)
        {
            // Lock position & rotation relative to the hand
            transform.localPosition = initialLocalPos;
            transform.localRotation = initialLocalRot;
        }
    }
}
