using UnityEngine;

public class CanvasFollower : MonoBehaviour
{
    [Header("References")]
    public static CanvasFollower Instance;
    public Transform player;             // XR Rig or player root
    public GameObject canvasObject;      // The main canvas (always active)
    public GameObject questionPanel;     // The panel to toggle ON/OFF
    public GameObject gameOverPanel;     // The panel to toggle ON/OFF
    public GameObject winPanel;          // The panel to toggle ON/OFF

    [Header("Settings")]
    public Vector3 offset = new Vector3(0, 1.5f, 2f); // Offset from player

    void Awake()
    {
        Instance = this;
    }

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

        if (questionPanel == null)
        {
            Debug.LogError("CanvasFollower: Question Panel reference not set!");
            return;
        }

        // Keep canvas active but hide questions at start
        canvasObject.SetActive(true);
        questionPanel.SetActive(false);
    }

    void Update()
    {
        // Always follow the player
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

    public void HideAllPanels()
    {
        questionPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    // Call this to show the question panel
    public void ShowQuestion()
    {
        questionPanel.SetActive(true);
    }

    // Call this to hide the question panel
    public void HideQuestion()
    {
        questionPanel.SetActive(false);
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(false);
        questionPanel.SetActive(false);
    }
}
