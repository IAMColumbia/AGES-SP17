using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsTextManager : MonoBehaviour
{
    Text knightStrengthText;
    Text knightStrengthText2;
    Text knightArmorText;
    Text knightArmorText2;
    Text necroStrengthText;
    Text necroStrengthText2;
    Text necroArmorText;
    Text necroArmorText2;

    Stats knight;
    Stats necro;

    void Start()
    {
        knightStrengthText = GameObject.Find("KnightStrengthText").GetComponent<Text>();
        knightStrengthText2 = GameObject.Find("KnightStrengthText2").GetComponent<Text>();
        knightArmorText = GameObject.Find("KnightArmorText").GetComponent<Text>();
        knightArmorText2 = GameObject.Find("KnightArmorText2").GetComponent<Text>();
        necroStrengthText = GameObject.Find("NecroStrengthText").GetComponent<Text>();
        necroStrengthText2 = GameObject.Find("NecroStrengthText2").GetComponent<Text>();
        necroArmorText = GameObject.Find("NecroArmorText").GetComponent<Text>();
        necroArmorText2 = GameObject.Find("NecroArmorText2").GetComponent<Text>();

        knight = GameObject.Find("Knight").GetComponent<Stats>();
        necro = GameObject.Find("Necro").GetComponent<Stats>();
    }

    void Update()
    {
        knightStrengthText.text = "Strength: " + knight.Strength;
        knightStrengthText2.text = "Strength: " + knight.Strength;

        knightArmorText.text = "Armor: " + knight.Armor;
        knightArmorText2.text = "Armor: " + knight.Armor;

        necroStrengthText.text = "Strength: " + necro.Strength;
        necroStrengthText2.text = "Strength: " + necro.Strength;

        necroArmorText.text = "Armor: " + necro.Armor;
        necroArmorText2.text = "Armor: " + necro.Armor;
    }
}
