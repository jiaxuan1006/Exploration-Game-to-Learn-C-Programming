using UnityEngine;
using System.Collections;

public class GlassFloor : MonoBehaviour
{
    public bool isCorrect;
    private QuestionManager questionManager;
    private bool hasCollided = false;
    private AudioSource audioSource;

    public AudioClip fallSound; // Assign this in the Inspector

    private void Start()
    {
        questionManager = FindObjectOfType<QuestionManager>();
        if (questionManager == null)
        {
            Debug.LogError("QuestionManager not found in the scene.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GlassFloor.");
        }
    }

    private void HandlePlayerCollision(GameObject player)
    {
        if (hasCollided) return;
        hasCollided = true;

        if (isCorrect)
        {
            // Mark question as answered
            questionManager.MarkQuestionAsAnswered(transform.position);
            questionManager.LoadNextQuestion();

            TaskManager taskManager = FindObjectOfType<TaskManager>();
            if (taskManager != null)
            {
                taskManager.AddScore(10);
            }
        }
        else
        {
            // Player falls logic
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.DecrementLives();
                playerController.RespawnPlayer();

                PlayFallSound(); // Play the fall sound
            }
        }

        // Reset the collision state after a short delay
        StartCoroutine(ResetCollisionState());
    }

    private IEnumerator ResetCollisionState()
    {
        yield return new WaitForSeconds(0.5f);
        hasCollided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision(collision.gameObject);
        }
    }

    private void PlayFallSound()
    {
        if (audioSource != null && fallSound != null)
        {
            audioSource.PlayOneShot(fallSound);
        }
    }
}