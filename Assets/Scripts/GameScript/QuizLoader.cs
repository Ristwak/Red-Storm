using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class Quiz
{
    public int id;
    public string question;
    public string[] options;
    public int correctIndex;
}

[System.Serializable]
public class QuizCollection
{
    public Quiz[] quizzes;
}

public class QuizLoader : MonoBehaviour
{
    [Header("UI References")]
    public Text questionText;          // Assign your Question Text UI
    public Button[] optionButtons;     // Assign 4 buttons for answers

    private QuizCollection quizData;
    private int currentIndex = 0;

    void Start()
    {
        LoadQuizData();
        ShowQuiz(currentIndex);
    }

    void LoadQuizData()
    {
        // Make sure mars_quiz.json is inside Assets/Resources/
        TextAsset jsonFile = Resources.Load<TextAsset>("mars_quiz");
        if (jsonFile != null)
        {
            quizData = JsonUtility.FromJson<QuizCollection>(jsonFile.text);
        }
        else
        {
            Debug.LogError("mars_quiz.json not found in Resources folder!");
        }
    }

    void ShowQuiz(int index)
    {
        if (quizData == null || index >= quizData.quizzes.Length) return;

        Quiz q = quizData.quizzes[index];

        // Set question
        questionText.text = q.question;

        // Set answers
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int optionIndex = i; // local copy for lambda
            optionButtons[i].GetComponentInChildren<Text>().text = q.options[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(optionIndex));
        }
    }

    void OnAnswerSelected(int chosenIndex)
    {
        Quiz q = quizData.quizzes[currentIndex];
        if (chosenIndex == q.correctIndex)
        {
            Debug.Log("✅ Correct!");
        }
        else
        {
            Debug.Log("❌ Wrong!");
        }

        // Load next question (or loop back to start)
        currentIndex++;
        if (currentIndex < quizData.quizzes.Length)
            ShowQuiz(currentIndex);
        else
            Debug.Log("All questions finished!");
    }
}
