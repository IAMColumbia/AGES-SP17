using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsTextManager : MonoBehaviour
{
    Text knightStrengthText;
    Text knightStrengthText2;
    Text knightArmorText;
    Text knightArmorText2;
    Text enemyStrengthText;
    Text enemyStrengthText2;
    Text enemyArmorText;
    Text enemyArmorText2;

    Stats knight;
    Stats enemy;

    void Start()
    {
        knightStrengthText = GameObject.Find("KnightStrengthText").GetComponent<Text>();
        knightStrengthText2 = GameObject.Find("KnightStrengthText2").GetComponent<Text>();
        knightArmorText = GameObject.Find("KnightArmorText").GetComponent<Text>();
        knightArmorText2 = GameObject.Find("KnightArmorText2").GetComponent<Text>();
        enemyStrengthText = GameObject.Find("NecroStrengthText").GetComponent<Text>();
        enemyStrengthText2 = GameObject.Find("NecroStrengthText2").GetComponent<Text>();
        enemyArmorText = GameObject.Find("NecroArmorText").GetComponent<Text>();
        enemyArmorText2 = GameObject.Find("NecroArmorText2").GetComponent<Text>();

        knight = GameObject.Find("Knight").GetComponent<Stats>();
        enemy = GameObject.Find("Necro").GetComponent<Stats>();
    }

    void Update()
    {
        knightStrengthText.text = "Strength: " + knight.Strength;
        knightStrengthText2.text = "Strength: " + knight.Strength;

        knightArmorText.text = "Armor: " + knight.Armor;
        knightArmorText2.text = "Armor: " + knight.Armor;

        enemyStrengthText.text = "Strength: " + enemy.Strength;
        enemyStrengthText2.text = "Strength: " + enemy.Strength;

        enemyArmorText.text = "Armor: " + enemy.Armor;
        enemyArmorText2.text = "Armor: " + enemy.Armor;
    }
}
