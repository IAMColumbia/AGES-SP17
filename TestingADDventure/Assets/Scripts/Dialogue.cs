using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    List<string> dialogueList = new List<string>();

    void Start()
    {
        GetComponent<Text>().text = "Okay, let's get started. ";

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
    }

    public void SetDialogueText(int listNum)
    {
        GetComponent<Text>().text = dialogueList[listNum];
    }
}
