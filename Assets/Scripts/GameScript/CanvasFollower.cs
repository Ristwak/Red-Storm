using UnityEngine;

public class CanvasFollower : MonoBehaviour
{
    [Header("References")]
    public Transform player;         // XR Rig or player root
    public GameObject canvasObject;  // Existing Question Canvas in the scene

    [Header("Settings")]
    public Vector3 offset = new Vector3(0, 1.5f, 2f); // Offset from player

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("CanvasFollower: Player reference not set!");
            return;
        }

        if (canvasObject == null)
        {
            Debug.LogError("CanvasFollower: Canvas reference not set!");
            return;
        }

        // Make sure it's hidden at start
        canvasObject.SetActive(false);
    }

    void Update()
    {
        if (!canvasObject.activeSelf) return; // Only update when active

        // Position canvas relative to player
        Vector3 targetPos = player.position
                          + player.forward * offset.z
                          + player.up * offset.y
                          + player.right * offset.x;
        canvasObject.transform.position = targetPos;

        // Match player's rotation but ignore Z axis tilt
        Vector3 euler = player.rotation.eulerAngles;
        euler.z = 0f; // lock roll
        canvasObject.transform.rotation = Quaternion.Euler(euler);
    }

    // Call this to show the canvas
    public void ShowCanvas()
    {
        canvasObject.SetActive(true);
    }

    // Call this to hide the canvas
    public void HideCanvas()
    {
        canvasObject.SetActive(false);
    }
}
