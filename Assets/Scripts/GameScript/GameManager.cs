using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float missionTime = 60f;          // total mission time in seconds
    private float timeRemaining;
    private bool isGameActive = true;

    [Header("UI References")]
    public TMP_Text timerText;               // Timer display
    public GameObject quizPanel;             // Assign your quiz panel (disabled by default)
    public GameObject gameOverPanel;         // Assign your GameOver panel (disabled by default)

    [Header("Quiz Settings")]
    public int numberOfPopups = 2;           // how many times quiz will appear
    private float[] quizTriggerTimes;        // times when quiz will appear
    private int quizIndex = 0;

    [Header("Player References")]
    public GameObject player;                // XR Rig or player root
    public MonoBehaviour playerMovementScript; // movement script to disable on game over

    void Start()
    {
        // Init timer
        timeRemaining = missionTime;
        gameOverPanel.SetActive(false);
        quizPanel.SetActive(false);

        // Pick random times for quiz popups
        quizTriggerTimes = new float[numberOfPopups];
        for (int i = 0; i < numberOfPopups; i++)
        {
            quizTriggerTimes[i] = Random.Range(missionTime * 0.2f, missionTime * 0.8f);
        }
        System.Array.Sort(quizTriggerTimes); // ensure they appear in order
    }

    void Update()
    {
        if (!isGameActive) return;

        // Update timer
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver();
        }

        // Update UI
        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

        // Trigger quizzes
        if (quizIndex < quizTriggerTimes.Length && timeRemaining <= quizTriggerTimes[quizIndex])
        {
            ShowQuiz();
            quizIndex++;
        }
    }

    void ShowQuiz()
    {
        quizPanel.SetActive(true);
        Time.timeScale = 0f; // pause gameplay while quiz is open
    }

    public void CloseQuiz()
    {
        quizPanel.SetActive(false);
        Time.timeScale = 1f; // resume gameplay
    }

    public void GameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;
        gameOverPanel.SetActive(true);

        // Disable movement
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;
    }

    // Call this from Sandstorm script when it hits player
    public void PlayerHitByStorm()
    {
        Debug.Log("Player hit by storm!");
        GameOver();
    }
}
