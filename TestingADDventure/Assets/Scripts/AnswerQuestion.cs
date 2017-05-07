using UnityEngine;
using System.Collections;

public class AnswerQuestion : MonoBehaviour
{
    [SerializeField]
    EnergyMeter energyMeter;
    [SerializeField]
    Dialogue dialogue;
    [SerializeField]
    GameObject dialogueBox;
    [SerializeField]
    GameObject dismissButton;
    [SerializeField]
    AudioSource pencilMark;

    bool shouldShowDialogue = true;

    public void AnswerButtonPressed()
    {
        if (energyMeter.startingEnergy == 9)
        {
            switch (energyMeter.energyLeft)
            {
                case 9:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = true;
                    break;
                case 8:
                    dialogue.SetDialogueText(1);
                    shouldShowDialogue = true;
                    break;
                case 7:
                    dialogue.SetDialogueText(2);
                    shouldShowDialogue = true;
                    break;
                case 6:
                    dialogue.SetDialogueText(3);
                    shouldShowDialogue = false;
                    break;
                case 5:
                    dialogue.SetDialogueText(4);
                    shouldShowDialogue = true;
                    break;
                case 4:
                    dialogue.SetDialogueText(5);
                    shouldShowDialogue = true;
                    break;
                case 3:
                    dialogue.SetDialogueText(6);
                    shouldShowDialogue = false;
                    break;
                case 2:
                    dialogue.SetDialogueText(7);
                    shouldShowDialogue = true;
                    break;
                case 1:
                    dialogue.SetDialogueText(7);
                    shouldShowDialogue = true;
                    break;
                case 0:
                    dialogue.SetDialogueText(7);
                    shouldShowDialogue = true;
                    break;
                default:
                    break;
            }
        }
        else if (energyMeter.startingEnergy == 15)
        {
            switch (energyMeter.energyLeft)
            {
                case 15:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 14:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = true;
                    break;
                case 13:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 12:
                    dialogue.SetDialogueText(1);
                    shouldShowDialogue = true;
                    break;
                case 11:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 10:
                    dialogue.SetDialogueText(2);
                    shouldShowDialogue = true;
                    break;
                case 9:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 8:
                    dialogue.SetDialogueText(3);
                    shouldShowDialogue = true;
                    break;
                case 7:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 6:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 5:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 4:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 3:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 2:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 1:
                    dialogue.SetDialogueText(0);
                    shouldShowDialogue = false;
                    break;
                case 0:
                    dialogue.SetDialogueText(4);
                    shouldShowDialogue = true;
                    break;
                default:
                    break;
            }
        }

        energyMeter.ReduceEnergy();
        pencilMark.Play();

        if (shouldShowDialogue)
        {
            dialogueBox.SetActive(true);
            dismissButton.SetActive(false);
            Invoke("DelayDismissButton", 0.8f);
        }
    }

    void DelayDismissButton()
    {
        dismissButton.SetActive(true);
    }
}
