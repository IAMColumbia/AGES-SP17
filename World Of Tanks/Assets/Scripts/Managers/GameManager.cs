using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> players;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Text winnerText;

    private PlayerSelect pSelect;
    public int numPlayers;

	void Start ()
    {
	    pSelect = GameObject.FindGameObjectWithTag("PlayerSelect").GetComponent<PlayerSelect>();
        numPlayers = pSelect.GetNumberOfPlayers();
        gameOverPanel.SetActive(false);
        CheckForPlayers(numPlayers);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (numPlayers <= 1)
        {
            DeclareWinner(players[0]);
        }
	}

    private void CheckForPlayers(int playerAmount)
    {
        switch (playerAmount)
        {
            case 2:
                Destroy(players[2]);
                Destroy(players[3]);
                break;
            case 3:
                Destroy(players[3]);
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    private void DeclareWinner(GameObject winner)
    {
        gameOverPanel.SetActive(true);
        winnerText.text = winner.name + " wins!";

    }
}
