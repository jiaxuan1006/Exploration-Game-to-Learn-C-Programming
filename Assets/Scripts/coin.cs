using UnityEngine;

public class coin : MonoBehaviour
{
    public AudioClip collectSound; // Sound to play when coin is collected
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player coin: touched" );
            CollectCoin(other.gameObject);

        }
    }

    private void CollectCoin(GameObject player)
    {
        // Play collection sound
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // Notify the player to increase coin count
        // player.GetComponent<ThirdPersonController>().CollectCoin();

        // Destroy the coin object after a delay to allow the sound to play
        Destroy(gameObject, collectSound != null ? collectSound.length : 0f);
    }
}