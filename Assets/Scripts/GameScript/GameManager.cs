using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float missionTime = 120f;
    private float timeRemaining;
    private bool isGameActive = true;

    [Header("UI References")]
    public TMP_Text timerText;
    public GameObject questionPanel;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public QuizLoader quizLoader;

    [Header("Player References")]
    public MonoBehaviour locomotionScript;
    public SandWallMover sandWallMover;   // ✅ assign your storm wall here

    private void Start()
    {
        timeRemaining = missionTime;
        CanvasFollower.Instance.HideAllPanels();

        // ✅ Show all questions at start
        ShowQuiz();
    }

    private void Update()
    {
        if (!isGameActive) return;

        // Countdown
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0;
            GameOver();
        }

        // Update UI
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void ShowQuiz()
    {
        questionPanel.SetActive(true);

        if (quizLoader != null)
        {
            quizLoader.gameManager = this;
            quizLoader.ShowQuiz(0);
        }

        if (locomotionScript != null)
            locomotionScript.enabled = false;
    }

    public void CloseQuiz()
    {
        questionPanel.SetActive(false);

        if (locomotionScript != null)
            locomotionScript.enabled = true;
    }

    public void PlayerWin()
    {
        if (!isGameActive) return;

        isGameActive = false;

        // ✅ stop storm
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

        // ✅ stop storm
        if (sandWallMover != null)
            sandWallMover.enabled = false;

        questionPanel.SetActive(false);
        gameOverPanel.SetActive(true);

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
