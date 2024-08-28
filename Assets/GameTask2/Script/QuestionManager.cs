using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public QuestionPair questionPairPrefab;
    public Transform playerTransform;
    public Question[] questions;

    private int currentQuestionIndex = 0;
    private QuestionPair currentQuestionPair;
    private Vector3 lastCorrectGlassFloorPosition;
    private bool[] answeredQuestions;
    private bool isPlayerRespawned = false;

    private void Start()
    {
        answeredQuestions = new bool[questions.Length];
        LoadNextQuestion(); // Load the first question
    }

    public void LoadNextQuestion()
    {
        if (isPlayerRespawned)
        {
            isPlayerRespawned = false; // Reset the respawn flag
            return;
        }

        // Hide the current question if it has been answered
        if (currentQuestionPair != null && answeredQuestions[currentQuestionIndex])
        {
            currentQuestionPair.HideQuestionAndAnswers();
            currentQuestionPair.HideIncorrectGlassFloor();
        }

        // Find the next unanswered question
        bool foundNextQuestion = false;
        int nextQuestionIndex = currentQuestionIndex;
        while (nextQuestionIndex < questions.Length)
        {
            if (!answeredQuestions[nextQuestionIndex])
            {
                foundNextQuestion = true;
                break;
            }
            nextQuestionIndex++;
        }

        if (foundNextQuestion)
        {
            currentQuestionIndex = nextQuestionIndex;
            Vector3 spawnPosition = new Vector3(0, 0, currentQuestionIndex * 5);
            QuestionPair newQuestionPair = Instantiate(questionPairPrefab, spawnPosition, Quaternion.identity);
            newQuestionPair.SetQuestion(questions[currentQuestionIndex]);
            currentQuestionPair = newQuestionPair;
        }
        else
        {
            Debug.Log("No more questions available.");
            // Handle end of game logic
        }
    }

    public void MarkQuestionAsAnswered(Vector3 correctGlassFloorPosition)
    {
        if (currentQuestionIndex < questions.Length)
        {
            answeredQuestions[currentQuestionIndex] = true;
            lastCorrectGlassFloorPosition = correctGlassFloorPosition;
        }
    }

    public Vector3 GetLastCorrectGlassFloorPosition()
    {
        return lastCorrectGlassFloorPosition;
    }

    public bool IsCurrentQuestionAnswered()
    {
        return answeredQuestions[currentQuestionIndex];
    }

    public void ReloadCurrentQuestion()
    {
        if (currentQuestionPair != null)
        {
            Destroy(currentQuestionPair.gameObject);
        }

        Vector3 spawnPosition = new Vector3(0, 0, currentQuestionIndex * 5);
        QuestionPair newQuestionPair = Instantiate(questionPairPrefab, spawnPosition, Quaternion.identity);
        newQuestionPair.SetQuestion(questions[currentQuestionIndex]);
        currentQuestionPair = newQuestionPair;
    }

    public void SetPlayerRespawned(bool respawned)
    {
        isPlayerRespawned = respawned;
    }

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }
}