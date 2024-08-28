using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private TaskManager taskManager;
    private CharacterController _controller;
    private QuestionManager questionManager;

    private bool isRespawning = false; // Track if respawn is already in progress
    private bool isFalling = false; // Track if the player is currently falling

    private void Start()
    {
        // Find the TaskManager and QuestionManager in the scene
        taskManager = FindObjectOfType<TaskManager>();
        questionManager = FindObjectOfType<QuestionManager>();
        _controller = GetComponent<CharacterController>();

        if (taskManager == null)
        {
            Debug.LogError("TaskManager not found.");
        }

        if (questionManager == null)
        {
            Debug.LogError("QuestionManager not found.");
        }
    }

    public void DecrementLives()
    {
        if (taskManager != null)
        {
            taskManager.DecrementLives();
            // Additional player logic...
        }
    }

    public void RespawnPlayer()
    {
        if (!isRespawning) // Check if respawn is already in progress
        {
            isRespawning = true; // Set the respawning flag

            if (questionManager != null)
            {
                Vector3 respawnPosition = questionManager.GetLastCorrectGlassFloorPosition();
                if (respawnPosition != Vector3.zero) // Check if the position is valid
                {
                    StartCoroutine(RespawnCoroutine(respawnPosition));
                }
                else
                {
                    Debug.LogError("Invalid respawn position, resetting to start.");
                    // Handle invalid respawn position if necessary
                }
            }
        }
    }

    private IEnumerator RespawnCoroutine(Vector3 respawnPosition)
    {
        _controller.enabled = false; // Disable controller
        yield return new WaitForSeconds(0.1f); // Wait a short time
        transform.position = respawnPosition + new Vector3(0, 1.63f, 0); // Adjust Y position
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f); // Reset rotation
        _controller.enabled = true; // Re-enable controller

        // Reload the current question
        if (questionManager != null)
        {
            questionManager.SetPlayerRespawned(true); // Set the respawn flag
            questionManager.ReloadCurrentQuestion();
        }

        isRespawning = false; // Reset the respawning flag
        isFalling = false; // Reset the falling flag
    }

    private void Update()
    {
        // Check if the player has fallen below the fallThreshold
        float fallThreshold = -10f;
        if (transform.position.y < fallThreshold && !isFalling)
        {
            HandleFall();
        }
    }

    private void HandleFall()
    {
        isFalling = true; // Set the falling flag
        DecrementLives();
        Debug.Log("Hi");
        RespawnPlayer();
    }
}