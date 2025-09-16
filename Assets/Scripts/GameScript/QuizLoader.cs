using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TMP_Text questionText;      // TextMeshPro question field
    public Button[] optionButtons;     // 4 buttons for answers

    private Quiz[] shuffledQuizzes;    // shuffled array
    private int currentIndex = 0;

    void Start()
    {
        LoadQuizData();
        ShuffleQuizzes();
        ShowQuiz(currentIndex);
    }

    void LoadQuizData()
    {
        // mars_quiz.json must be inside Assets/Resources/
        TextAsset jsonFile = Resources.Load<TextAsset>("mars_quiz");
        if (jsonFile != null)
        {
            QuizCollection quizData = JsonUtility.FromJson<QuizCollection>(jsonFile.text);
            shuffledQuizzes = quizData.quizzes;
        }
        else
        {
            Debug.LogError("mars_quiz.json not found in Resources folder!");
        }
    }

    void ShuffleQuizzes()
    {
        if (shuffledQuizzes == null || shuffledQuizzes.Length == 0) return;

        for (int i = 0; i < shuffledQuizzes.Length; i++)
        {
            int rand = Random.Range(i, shuffledQuizzes.Length);
            Quiz temp = shuffledQuizzes[i];
            shuffledQuizzes[i] = shuffledQuizzes[rand];
            shuffledQuizzes[rand] = temp;
        }
    }

    void ShowQuiz(int index)
    {
        if (shuffledQuizzes == null || index >= shuffledQuizzes.Length) return;

        Quiz q = shuffledQuizzes[index];

        // Set question
        questionText.text = q.question;

        // Set answers
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int optionIndex = i; // local copy for lambda
            TMP_Text btnText = optionButtons[i].GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = q.options[i];

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(optionIndex));
        }
    }

    void OnAnswerSelected(int chosenIndex)
    {
        Quiz q = shuffledQuizzes[currentIndex];
        if (chosenIndex == q.correctIndex)
        {
            Debug.Log("✅ Correct!");
        }
        else
        {
            Debug.Log("❌ Wrong!");
        }

        // Load next question
        currentIndex++;
        if (currentIndex < shuffledQuizzes.Length)
            ShowQuiz(currentIndex);
        else
            Debug.Log("All questions finished!");
    }
}
