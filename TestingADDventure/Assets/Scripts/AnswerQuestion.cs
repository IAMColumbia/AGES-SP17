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
        if (energyMeter.startingEnergy == 7)
        {
            switch (energyMeter.energyLeft)
            {
                case 7:
                    dialogue.SetDialogueText(0);
                    break;
                case 6:
                    dialogue.SetDialogueText(1);
                    break;
                case 5:
                    dialogue.SetDialogueText(2);
                    break;
                case 4:
                    dialogue.SetDialogueText(3);
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
