using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LobbyManager_S : MonoBehaviour
{
    public Text playerName; // Reference to the Text component for displaying the player's name
    public APISystem api; // Reference to the APISystem script for API interactions

    public Text enterPlayerName; // Reference to the input field for entering the player's name

    public GameObject roomSettingPanel; // Reference to the panel for setting up the room
    public GameObject waiting; // Reference to the waiting UI element

    public Button startButton; // Reference to the button for starting the game

    void Start()
    {
        // Initialization code can be added here
    }

    void Update()
    {
        // Update code can be added here
    }

    public void JoinRoom()
    {
        if (enterPlayerName.text != "")
        {
            // Hide the room setting panel and show the waiting UI element
            roomSettingPanel.SetActive(false);
            waiting.SetActive(true);
            startButton.interactable = true; // Enable the start button

            // Store the player's name using PlayerPrefs
            PlayerPrefs.SetString("username", enterPlayerName.text);

            // Display the player's name in the playerName Text component
            playerName.text = enterPlayerName.text;

            // Register the player's name with the Tenenet API
            FindObjectOfType<APISystem>().Register(enterPlayerName.text, enterPlayerName.text, enterPlayerName.text, enterPlayerName.text);

            // Log the player's name to the console for debugging
            Debug.Log("Player Name : " + PlayerPrefs.GetString("username"));
            Debug.Log(enterPlayerName.text); // Log the player's name to the console
        }
        else
        {
            startButton.interactable = false; // Disable the start button if player name is not entered
        }
    }
}
