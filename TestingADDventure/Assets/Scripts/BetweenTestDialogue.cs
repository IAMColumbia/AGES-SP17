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
            dialogueList.Add("I’ve started taking medication that should help me focus. ");
            dialogueList.Add("It has been working well enough so far but it has been making me feel jittery. ");
            dialogueList.Add("I can’t sit still anymore and I keep biting my lip. ");
            dialogueList.Add("I keep taking short breaths and tapping my finger on the table too. ");
            dialogueList.Add("The worst part is that I have no appetite. ");
            dialogueList.Add("I have to force myself to to eat and even then I can barely stomach a bowl of cereal. ");
            dialogueList.Add("It’s all really annoying but it’s worth it if it means I can pay attention. ");
            dialogueList.Add("I guess. ");
        }
        else if (sceneNumber == 5)
        {
            dialogueList.Clear();

            //Dialogue 3
            dialogueList.Add("I’ve been taking this medication for a while now. ");
            dialogueList.Add("Even still the jitters haven’t really gotten any less annoying. ");
            dialogueList.Add("Eating substantial meals is still a struggle. ");
            dialogueList.Add("Other than that the medication has been doing its job so I guess it’s worthwhile to keep taking it. ");
            dialogueList.Add("I’ve been told that many people with ADD no longer need medication after they finish school. ");
            dialogueList.Add("So far it’s not looking like that will be the case for me but I should try to keep a positive outlook. ");
            dialogueList.Add("It will be tough; as tough as it has always been. ");
            dialogueList.Add("Having ADD and taking these medications are both a part of my life now. ");
            dialogueList.Add("That’s just something I have to accept. ");
            dialogueList.Add("That sounds like something I can do. ");
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