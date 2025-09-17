using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float missionTime = 120f;          // total mission time in seconds
    private float timeRemaining;
    private bool isGameActive = true;

    [Header("UI References")]
    public TMP_Text timerText;               // Timer display
    public GameObject quizPanel;             // Assign your quiz panel (disabled by default)
    public GameObject gameOverPanel;         // Assign your GameOver panel (disabled by default)
    public QuizLoader quizLoader;            // Reference to QuizLoader component

    [Header("Quiz Settings")]
    public float quiz1Time = 90f;            // when first quiz should appear (seconds left)
    public float quiz2Time = 45f;            // when second quiz should appear (seconds left)
    private int quizIndex = 0;
    private bool isQuizActive = false;

    [Header("Player References")]
    public MonoBehaviour locomotionScript;   // movement script (leave hand scripts alone)

    private float[] quizTriggerTimes;

    void Start()
    {
        // Init timer
        timeRemaining = missionTime;
        gameOverPanel.SetActive(false);
        quizPanel.SetActive(false);

        // Assign quiz times from inspector
        quizTriggerTimes = new float[2] { quiz1Time, quiz2Time };
        System.Array.Sort(quizTriggerTimes); // keep them in order
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

        // Update UI with MM:SS format
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // Trigger quizzes at your chosen times
        if (!isQuizActive && quizIndex < quizTriggerTimes.Length && timeRemaining <= quizTriggerTimes[quizIndex])
        {
            ShowQuiz();
            quizIndex++;
        }
    }

    void ShowQuiz()
    {
        isQuizActive = true;
        quizPanel.SetActive(true);

        if (quizLoader != null)
            quizLoader.ShowQuiz(quizIndex); // tell loader which quiz to show

        // Disable locomotion only (hands still work)
        if (locomotionScript != null)
            locomotionScript.enabled = false;
    }

    public void CloseQuiz()
    {
        isQuizActive = false;
        quizPanel.SetActive(false);

        // Re-enable locomotion
        if (locomotionScript != null)
            locomotionScript.enabled = true;
    }

    public void GameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;
        gameOverPanel.SetActive(true);

        if (locomotionScript != null)
            locomotionScript.enabled = false;
    }

    public void PlayerHitByStorm()
    {
        Debug.Log("Player hit by storm!");
        GameOver();
    }
}
