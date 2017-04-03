using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int numRoundsToWin = 3;
    [SerializeField]
    Text messageText;
    [SerializeField]
    CameraController cameraControl;
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    PlayerManager[] players;
    [SerializeField]
    Transform[] playerSpawnPoints;

    float startDelay = 3f;
    float endDelay = 3f;
    
    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;


    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllTanks();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllTanks()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].instance =
                Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            players[i].playerNumber = i + 1;
            players[i].Setup();
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();

        roundNumber++;
        messageText.text = "Round " + roundNumber;

        yield return startWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl();

        messageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();

        roundWinner = null;

        roundWinner = GetRoundWinner();

        if (roundWinner != null)
            roundWinner.wins++;

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;


        yield return endWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }


    private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].instance.activeSelf)
                return players[i];
        }

        return null;
    }


    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].wins == numRoundsToWin)
                return players[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (roundWinner != null)
            message = roundWinner.coloredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < players.Length; i++)
        {
            message += players[i].coloredPlayerText + ": " + players[i].wins + " WINS\n";
        }

        if (gameWinner != null)
            message = gameWinner.coloredPlayerText + " WINS THE GAME!";

        return message;
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }
}
