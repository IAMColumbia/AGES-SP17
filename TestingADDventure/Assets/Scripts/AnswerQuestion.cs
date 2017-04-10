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

    public void AnswerButtonPressed()
    {
        //Debug.Log(energyMeter.startingEnergy + " starting");
        if (energyMeter.startingEnergy == 7)
        {
            //Debug.Log(energyMeter.energyLeft + " left before");
            switch (energyMeter.energyLeft)
            {
                case 7:
                    //Debug.Log("bleh");
                    dialogue.SetDialogueText(0);
                    //Debug.Log(energyMeter.energyLeft + " left after 7");
                    break;
                case 6:
                    dialogue.SetDialogueText(1);
                    //Debug.Log(energyMeter.energyLeft + " left after 6");
                    break;
                case 5:
                    dialogue.SetDialogueText(2);
                    //Debug.Log(energyMeter.energyLeft + " left after 5");
                    break;
                case 4:
                    dialogue.SetDialogueText(3);
                    //Debug.Log(energyMeter.energyLeft + " left after 4");
                    break;
                case 3:
                    dialogue.SetDialogueText(4);
                    break;
                case 2:
                    dialogue.SetDialogueText(5);
                    break;
                case 1:
                    dialogue.SetDialogueText(6);
                    break;
                case 0:
                    dialogue.SetDialogueText(7);
                    break;
                default:
                    break;
            }
        }
        else if (energyMeter.startingEnergy == 8)
        {
            switch (energyMeter.energyLeft)
            {
                case 8:
                    dialogue.SetDialogueText(0);
                    break;
            }
        }
        else if (energyMeter.startingEnergy == 10)
        {
            switch (energyMeter.energyLeft)
            {
                case 10:
                    dialogue.SetDialogueText(0);
                    break;
            }
        }

        energyMeter.ReduceEnergy();
        dialogueBox.SetActive(true);
    }
}
