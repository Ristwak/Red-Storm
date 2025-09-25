using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float missionTime = 120f;
    private float timeRemaining;
    private bool isGameActive = false;  // Game should not be active until start button is clicked

    [Header("UI References")]
    public TMP_Text timerText;
    public GameObject questionPanel;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject startPanel;       // The start panel with Start and Quit buttons
    public QuizLoader quizLoader;

    [Header("Player References")]
    public MonoBehaviour locomotionScript;
    public SandWallMover sandWallMover;   // ✅ assign your storm wall here

    private void Start()
    {
        // Initially hide all panels
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        questionPanel.SetActive(false);
        startPanel.SetActive(true);  // Show the start panel

        // Disable the sandstorm and timer until start
        if (sandWallMover != null)
            sandWallMover.enabled = false;  // Pause the sandstorm

        // Disable movement
        if (locomotionScript != null)
            locomotionScript.enabled = false;
    }

    private void Update()
    {
        if (!isGameActive) return;

        // Countdown timer only runs when game is active
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0;
            GameOver();
        }

        // Update timer UI
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Start the game when Start button is clicked
    public void StartGame()
    {
        // Hide start panel
        startPanel.SetActive(false);

        // Start game functionality
        isGameActive = true;
        timeRemaining = missionTime;

        // Start the quiz and disable movement for the player during the quiz
        ShowQuiz();

        // Start the sandstorm
        if (sandWallMover != null)
            sandWallMover.enabled = true;  // Unpause the sandstorm

        // Enable player movement
        if (locomotionScript != null)
            locomotionScript.enabled = true;
    }

    void ShowQuiz()
    {
        questionPanel.SetActive(true);

        if (quizLoader != null)
        {
            quizLoader.gameManager = this;
            quizLoader.ShowQuiz(0);
        }

        // Disable player movement during quiz
        if (locomotionScript != null)
            locomotionScript.enabled = false;
    }

    public void CloseQuiz()
    {
        questionPanel.SetActive(false);

        // Enable player movement after quiz
        if (locomotionScript != null)
            locomotionScript.enabled = true;
    }

    public void PlayerWin()
    {
        if (!isGameActive) return;

        isGameActive = false;

        // Stop storm
        if (sandWallMover != null)
            sandWallMover.enabled = false;

        // Show win panel
        if (winPanel != null)
            winPanel.SetActive(true);

        // Disable movement
        if (locomotionScript != null)
            locomotionScript.enabled = false;

        Debug.Log("🎉 Mission Complete! You reached the base.");
    }

    public void GameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;

        // Stop storm
        if (sandWallMover != null)
            sandWallMover.enabled = false;

        questionPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        // Disable movement
        if (locomotionScript != null)
            locomotionScript.enabled = false;
    }

    public void PlayerHitByStorm()
    {
        Debug.Log("Player hit by storm!");
        GameOver();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
