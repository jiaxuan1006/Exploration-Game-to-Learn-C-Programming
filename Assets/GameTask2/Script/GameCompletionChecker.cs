using UnityEngine;

public class GameCompletionChecker : MonoBehaviour
{
    public TaskManager taskManager; // Assign this in the Inspector
    public Transform completionPlatform; // Assign the completion platform in the Inspector

    private void Update()
    {
        // Check if the player is at the completion platform
        if (Vector3.Distance(transform.position, completionPlatform.position) < 1.0f)
        {
            taskManager.CompleteGame();
        }
    }
}