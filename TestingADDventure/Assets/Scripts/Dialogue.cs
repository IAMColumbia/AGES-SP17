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
    [SerializeField]
    int sceneNumber;

    List<string> dialogueList = new List<string>();

    void Start()
    {
        if (sceneNumber == 2)
        {
            dialogueList.Clear();

            //Test 1
            dialogueList.Add("That one was easy. I got this. "); //0
            dialogueList.Add("I can't get this song out of my head. "); //1
            dialogueList.Add("I just want to go home and play video games. "); //2
            dialogueList.Add("I'm not sure if that's right but I need to keep going. "); //3
            dialogueList.Add("That's probably right. How much time do I have left? "); //4
            dialogueList.Add("No that's definitely wrong. I should change my answer. "); //5
            dialogueList.Add("This is really boring. "); //6
            dialogueList.Add("I can't think straight. "); //7
            dialogueList.Add("I need to focus. "); //8
            dialogueList.Add("This is really difficult now. "); //9
            dialogueList.Add("I'm hungry. "); //10
            dialogueList.Add("Dialogue 12"); //11
            dialogueList.Add("Ugh, I can't focus on this anymore. "); //12
            dialogueList.Add("Crap, I didn't finish. "); //13
        }
        else if (sceneNumber == 4)
        {
            dialogueList.Clear();

            //Test 2
            dialogueList.Add("Ugh, I can’t stop tapping my finger on the desk. "); //0
            dialogueList.Add("It’s almost as distracting as not being on the meds. "); //1
            dialogueList.Add("This is really making my finger hurt. "); //2
            dialogueList.Add("At least I can think straight. "); //3
        }
    }

    public void SetDialogueText(int listNum)
    {
        dialogueText.text = dialogueList[listNum];
    }
}
