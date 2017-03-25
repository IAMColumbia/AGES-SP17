using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    GameObject endPanel;

    public List<Slider> playerHealthBars = new List<Slider>();
    [SerializeField]
    int numberOfPlayersAlive;
    int winningPlayer;
    AbilitySelect AbSelect;

    void Start()
    {
        AbSelect = GameObject.Find("AbilitySelector").GetComponent<AbilitySelect>();
        numberOfPlayersAlive = AbSelect.numberOfPlayers;
    }

    public void OnDeath()
    {
        for (int i = 0; i < playerHealthBars.Count; i++)
        {
            if (playerHealthBars[i].value == 0)
            {
                numberOfPlayersAlive--;
                playerHealthBars.Remove(playerHealthBars[i]);
            }
        }

        if (numberOfPlayersAlive == 1)
        {
            foreach (Slider healthBar in playerHealthBars)
            {
                if (healthBar.value > 0)
                {
                    winningPlayer = healthBar.transform.parent.GetChild(2).GetComponent<AbilityCooldown>().playerNumber;
                }
            }

            endPanel.GetComponentInChildren<Text>().text = "Player " + winningPlayer + " Wins!";
            endPanel.SetActive(true);
        }
    }

    public void ReturnToMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
