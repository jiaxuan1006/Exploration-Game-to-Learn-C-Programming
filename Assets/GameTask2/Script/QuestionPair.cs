using UnityEngine;
using UnityEngine.UI;

public class QuestionPair : MonoBehaviour
{
    public Text questionText;
    public Text[] answerTexts;
    public GlassFloor[] glassFloors;

    public void SetQuestion(QuestionManager.Question question)
    {
        questionText.text = question.questionText;

        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = question.answers[i];
            glassFloors[i].isCorrect = false; // Initialize all floors as incorrect
            SetGlassFloorTrigger(glassFloors[i], true); // Set all floors as triggers
        }

        int correctAnswerIndex = question.correctAnswerIndex;
        answerTexts[correctAnswerIndex].text = question.answers[correctAnswerIndex];
        glassFloors[correctAnswerIndex].isCorrect = true; // Set the correct floor
        SetGlassFloorTrigger(glassFloors[correctAnswerIndex], false); // Disable trigger for the correct floor
    }

    public void HideQuestionAndAnswers()
    {
        questionText.gameObject.SetActive(false);
        foreach (var answerText in answerTexts)
        {
            answerText.gameObject.SetActive(false);
        }
    }

    public void HideIncorrectGlassFloor()
    {
        foreach (var glassFloor in glassFloors)
        {
            if (!glassFloor.isCorrect)
            {
                glassFloor.gameObject.SetActive(false);
            }
        }
    }

    private void SetGlassFloorTrigger(GlassFloor glassFloor, bool isTrigger)
    {
        Collider collider = glassFloor.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = isTrigger;
        }
    }
}