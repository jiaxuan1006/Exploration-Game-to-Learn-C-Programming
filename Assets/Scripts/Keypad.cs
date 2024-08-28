using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadyPlayerMe.Samples.QuickStart; // Add this line


public class Keypad : MonoBehaviour
{
    [SerializeField] private Text Ans;
    [SerializeField] private Animator Box;
    [SerializeField] private GameObject toDeactive;
    [SerializeField] private CollideTreasure collideTreasure; // Add a reference to the CollideTreasure script
    [SerializeField] private ThirdPersonController playerController;
    


    private string Answer = "1234";

    public void Number(int number)
    {
        Ans.text += number.ToString();
    }

    public void Execute()
    {
        if (Ans.text == Answer)
        {
            Ans.text = "Correct";                           
            Box.SetBool("Open", true);
            collideTreasure.ShowAnswer(); // Show the answer panel when the answer is correct
            
            // Add coins for correct answer
            playerController.playerCoins += 1;
            PlayerPrefs.SetInt("PlayerCoins", playerController.playerCoins);
            playerController.coinText.text = "Coin: " + playerController.playerCoins.ToString();
            StartCoroutine("StopDoor");
        }
        else
        {
            Ans.text = "Invalid";
            StartCoroutine("Erase");
        }
    }

    public void Cancel()
    {
        Ans.text = "";
        toDeactive.SetActive(false);
    }

    IEnumerator StopDoor()
    {
        toDeactive.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //Box.SetBool("Open", false);
        Box.enabled = false;
    }

    IEnumerator Erase()
    {
        yield return new WaitForSeconds(0.5f);
        Ans.text = "";
    }
}