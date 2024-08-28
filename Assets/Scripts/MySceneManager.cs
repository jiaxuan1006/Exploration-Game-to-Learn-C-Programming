using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;
using UnityEngine.UI;
using ReadyPlayerMe.Samples.QuickStart;

public class MySceneManager : MonoBehaviour
{
    private int currentCoin;
    public GameObject popupPanel;
    public Button okayButton;
    public Text coinsText;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to close the app when a button is clicked
    public void CloseApp()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName == "Quiz Game" || sceneName == "Squid Game")
        {
            currentCoin = PlayerPrefs.GetInt("PlayerCoins", 0);
            if(currentCoin > 2)
            {
                Debug.Log("Minus 3 coins");
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
                player.GetComponent<ThirdPersonController>().PayCoins();
            }
            else
            {
                popupPanel.SetActive(true);
            }
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    //public void QuizProgress()
    //{
    //    PlayerPrefs.SetInt("ProgressQuiz", 1);
    //}
}
