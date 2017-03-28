using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGamePanelController : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    Image firstRoundWinIndicator;
    [SerializeField]
    Image secondRoundWinIndicator;
    [SerializeField]
    Image thirdRoundWinIndicator;
    [SerializeField]
    GameManager gameManager;

    void Update()
    {
        UpdateRoundsWonIndicator();
    }

    void UpdateRoundsWonIndicator()
    {
        if (gameManager.Players[playerNumber - 1].wins == 1)
        {
            firstRoundWinIndicator.color = gameManager.Players[playerNumber - 1].playerColor;
        }
        else if (gameManager.Players[playerNumber - 1].wins == 2)
        {
            firstRoundWinIndicator.color = gameManager.Players[playerNumber - 1].playerColor;
            secondRoundWinIndicator.color = gameManager.Players[playerNumber - 1].playerColor;
        }
        else if (gameManager.Players[playerNumber - 1].wins == 3)
        {
            firstRoundWinIndicator.color = gameManager.Players[playerNumber - 1].playerColor;
            secondRoundWinIndicator.color = gameManager.Players[playerNumber - 1].playerColor;
            thirdRoundWinIndicator.color = gameManager.Players[playerNumber - 1].playerColor;
        }
    }
    
}
