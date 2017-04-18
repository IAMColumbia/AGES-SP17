using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    Text dialogueText;
    [SerializeField]
    int dialogueNumber;

    List<string> dialogueList = new List<string>();

    void Start()
    {
        //Test 1
        dialogueList.Add("That one was easy. I got this. "); //0
        dialogueList.Add("This is pretty simple so far. "); //1
        dialogueList.Add("Ok I think that’s right. Next question."); //2
        dialogueList.Add("I'm not sure if that's right but I need to keep going. "); //3
        dialogueList.Add("That's probably right. How much time do I have left? "); //4
        dialogueList.Add("This is really boring. "); //5
        dialogueList.Add("Alright this is getting more difficult. "); //6
        dialogueList.Add("I can't think straight. "); //7
        dialogueList.Add("I need to focus. "); //8
        dialogueList.Add("This is really difficult now. "); //9
        dialogueList.Add("I'm hungry. "); //10
        dialogueList.Add("Dialogue 12"); //11
        dialogueList.Add("Ugh, I can't focus on this anymore. "); //12
        dialogueList.Add("Crap, I didn't finish. "); //13

        //Between Tests Transition
        dialogueList.Add("1 test\ntest"); //14
        dialogueList.Add("2 test\ntest"); //15
        dialogueList.Add("3 test\ntest"); //16
        dialogueList.Add("4 test\ntest"); //17
        dialogueList.Add("5 test\ntest"); //18
        dialogueList.Add("6 test\ntest"); //19
        dialogueList.Add("7 test\ntest"); //20
        dialogueList.Add("8 test\ntest"); //21
    }

    public void SetDialogueText(int listNum)
    {
        dialogueText.text = dialogueList[listNum];
    }

    public void NextDialogue()
    {
        dialogueNumber++;
        SetDialogueText(dialogueNumber);
    }
}
