using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTreasure : MonoBehaviour
{
    [SerializeField] private GameObject toActivate;
    [SerializeField] private GameObject KeypadActive;
    [SerializeField] private GameObject questionPanel; // Add a reference for the question panel
    [SerializeField] private GameObject answerPanel; // Add a reference for the answer panel

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            toActivate.SetActive(false);
            KeypadActive.SetActive(true);
            ShowQuestion(); // Show the question when the keypad is active
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Treasure"))
        {
            Debug.Log("Near treasure");
            toActivate.SetActive(true);
            questionPanel.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Treasure"))
        {
            Debug.Log("Left treasure");
            toActivate.SetActive(false);
            questionPanel.SetActive(false);
            KeypadActive.SetActive(false);
            answerPanel.SetActive(false); // Deactivate the answer panel when the user leaves
        }
    }

    private void ShowQuestion()
    {
        questionPanel.SetActive(true); // Activate the question panel
    }
    


    public void ShowAnswer()
    {
        questionPanel.SetActive(false); // Deactivate the question panel
        answerPanel.SetActive(true); // Activate the answer panel
    }
}