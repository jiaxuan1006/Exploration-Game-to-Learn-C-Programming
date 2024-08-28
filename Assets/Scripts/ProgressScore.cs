using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScore : MonoBehaviour
{
    [SerializeField] private Image uiFillImage;
    [SerializeField] private Text uiStartText;
    [SerializeField] private Text uiEndText;
    private int score = 0;
    private float progress = 0f;
    public Text scoreText;
    private int squidProgressAdded; 
    private int quizProgressAdded;

    void Start()
    {
        score = score + PlayerPrefs.GetInt(GameUtility.SavePrefKey); //Load highest quiz score
        score = score + PlayerPrefs.GetInt("Score", 0); // Load the score from PlayerPrefs (squid)
        UpdateScoreText();
        UpdateProgressFill(progress);
        AddProgressSquid();
        AddProgressQuiz();
    }

    public void AddProgressSquid()
    {
        squidProgressAdded = PlayerPrefs.GetInt("ProgressSquid", 0);

        if (squidProgressAdded == 1)
        {
            progress += 0.5f;
            UpdateProgressFill(progress);
        }
    }

    public void AddProgressQuiz()
    {
        quizProgressAdded = PlayerPrefs.GetInt("ProgressQuiz", 0);

        if (quizProgressAdded == 1)
        {
            progress += 0.5f;
            UpdateProgressFill(progress);
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }
}