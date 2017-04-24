using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BetweenTestDialogue : MonoBehaviour
{
    [SerializeField]
    Text dialogueText;
    [SerializeField]
    GameObject nextButton;
    [SerializeField]
    GameObject previousButton;
    [SerializeField]
    GameObject continueButton;
    [SerializeField]
    string nextScene;
    [SerializeField]
    int sceneNumber;

    List<string> dialogueList = new List<string>();
    int dialogueNum = 0;

    void Start()
    {
        if (sceneNumber == 1)
        {
            dialogueList.Clear();

            //Dialogue 1
            dialogueList.Add("I’ve got a math test today. I hope it goes well. ");
            dialogueList.Add("Who am I kidding? Of course it’s not going to go well. I’m terrible at taking tests. ");
            dialogueList.Add("I know it’s because of my ADD, or, attention deficit disorder, as my doctor called it. ");
            dialogueList.Add("No one has told me what to do about my ADD though. ");
            dialogueList.Add("They just say “I guess you’ll just have to focus and study hard.” ");
            dialogueList.Add("That’s exactly what I’ve been doing but it’s never enough. ");
            dialogueList.Add("I’ve told them that but they don’t seem understand what having ADD feels like. ");
            dialogueList.Add("I don’t think there is anything I can do about it right now so let’s just get this over with. ");
        }
        else if (sceneNumber == 3)
        {
            dialogueList.Clear();

            //Dialogue 2
            dialogueList.Add("0");
            dialogueList.Add("1");
            dialogueList.Add("2");
            dialogueList.Add("3");
            dialogueList.Add("4");
        }
        else if (sceneNumber == 5)
        {
            dialogueList.Clear();

            //Dialogue 3
            dialogueList.Add("5");
            dialogueList.Add("6");
            dialogueList.Add("7");
            dialogueList.Add("8");
            dialogueList.Add("9");
        }

        dialogueText.text = dialogueList[dialogueNum];

        previousButton.SetActive(false);
        continueButton.SetActive(false);
        nextButton.SetActive(true);
    }

    public void NextButtonPressed()
    {
        //fade out animation
        dialogueNum++;
        dialogueText.text = dialogueList[dialogueNum];
        //fade in animation

        if (dialogueNum >= 1)
            previousButton.SetActive(true);

        if (dialogueNum >= dialogueList.Count - 1)
        {
            continueButton.SetActive(true);
            nextButton.SetActive(false);
        }
    }

    public void PreviousButtonPressed()
    {
        //fade out animation
        dialogueNum--;
        dialogueText.text = dialogueList[dialogueNum];
        //fade in animation

        if (dialogueNum <= 0)
            previousButton.SetActive(false);

        if (dialogueNum <= dialogueList.Count + 1)
        {
            continueButton.SetActive(false);
            nextButton.SetActive(true);
        }
    }

    public void ContinueButtonPressed()
    {
        SceneManager.LoadScene(nextScene);
    }
}