using UnityEngine;

public class WatchAttachment : MonoBehaviour
{
    [Header("Attach To Hand")]
    public Transform handTransform;   // Assign LeftHand or RightHand in Inspector

    [Header("Position Offset")]
    public Vector3 positionOffset;    // Adjust this in Inspector until the watch looks right
    public Vector3 rotationOffset;    // Adjust rotation for proper alignment

    private void Start()
    {
        if (handTransform != null)
        {
            // Parent the watch to the hand
            transform.SetParent(handTransform);

            // Reset local position & rotation then apply offsets
            transform.localPosition = positionOffset;
            transform.localEulerAngles = rotationOffset;
        }
        else
        {
            Debug.LogWarning("WatchAttachment: No hand assigned!");
        }
    }
}
