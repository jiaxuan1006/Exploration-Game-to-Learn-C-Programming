using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TaskManager : MonoBehaviour
{
    public APISystem api;
    public int score = 0;
    public int lives = 3;
    public float gameTime = 60f; // Total time for the game
    public Text scoreText;
    public Text livesText;
    public Text timerText;
    public GameObject gameOverPanel; // UI panel for Game Over
    public GameObject completionPanel; // UI panel for Game Completion
    private bool isGameOver = false;
    public GameObject highScoreBadge; // UI element for High Score Badge
    public GameObject speedRunBadge; // UI element for Speed Run Badge
    public GameObject firstCompletionBadge; // UI element for First Completion Badge
    private Vector3 lastCorrectGlassFloorPosition = Vector3.zero;

    private void Start()
    {
        UpdateUI();
        StartCoroutine(TimerCoroutine());
        gameOverPanel.SetActive(false);
        completionPanel.SetActive(false);
        highScoreBadge.SetActive(false);
        speedRunBadge.SetActive(false);
        firstCompletionBadge.SetActive(false);
    }

    private void SetHighscore(int currentScore)
    {
        var highscore = PlayerPrefs.GetInt("Score", 0);
        FindObjectOfType<APISystem>().InsertPlayerActivity(PlayerPrefs.GetString("username"), "score_Task2", "add", currentScore.ToString());
        if (highscore < currentScore)
        {
            PlayerPrefs.SetInt("Score", currentScore);
        }
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            UpdateUI();
        }
    }

    public void MinusScore(int points)
    {
        if (!isGameOver)
        {
            score -= points;
            UpdateUI();
        }
    }

    public void DecrementLives()
    {
        if (!isGameOver)
        {
            lives--;
            score -= 10;
            UpdateUI();
            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over!");
    }

    public void CompleteGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        completionPanel.SetActive(true); // Show the completion panel
        Debug.Log("Game Completed!");
        //Update the player score inside TenenetAPI
        SetHighscore(score); // Save the highest score
        // Award badges
        AwardCompletionBadges();
        PlayerPrefs.SetInt("ProgressSquid", 1);
        StartCoroutine(TransitionToMainScene());
    }

    private IEnumerator TransitionToMainScene()
    {
        yield return new WaitForSeconds(3); // Wait for 3 seconds
        SceneManager.LoadScene("Main Scene"); 
    }

    private IEnumerator TimerCoroutine()
    {
        while (gameTime > 0 && !isGameOver)
        {
            yield return new WaitForSeconds(1f);
            gameTime--;
            timerText.text = "Time: " + gameTime;

            if (gameTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void SetLastCorrectGlassFloorPosition(Vector3 position)
    {
        lastCorrectGlassFloorPosition = position;
    }

    public Vector3 GetLastCorrectGlassFloorPosition()
    {
        return lastCorrectGlassFloorPosition;
    }


    // Function to award badges based on completion conditions
    private void AwardCompletionBadges()
    {
        // Example logic to award badges
        if (score >= 100)
        {
            AwardBadge("HighScore", highScoreBadge);
        }

        if (gameTime >= 20)
        {
            AwardBadge("SpeedRun", speedRunBadge);
        }

        AwardBadge("FirstCompletion", firstCompletionBadge);
    }

    // Function to log the badge awarding and update the UI
    private void AwardBadge(string badgeName, GameObject badgeUI)
    {
        if (badgeUI != null)
        {
            badgeUI.SetActive(true); // Show the badge UI element
        }
        Debug.Log("Badge Awarded: " + badgeName);
        // Save the badge award to player preferences (or another storage system)
        PlayerPrefs.SetString(badgeName, "awarded");
    }
}