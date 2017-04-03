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
    CameraController mainCamera;
    [SerializeField]
    LiftRampsAfterStart[] allRamps;
    [SerializeField]
    DropRingAfterTime[] allStagePieces;
    [SerializeField]
    PlayerManager[] players;
    public PlayerManager[] Players
    {
        get { return players; }
    }

    float startDelay = 3f;
    float endDelay = 3f;
    
    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

    private void Start()
    {
        //informationNeededFromMenu = GameObject.Find("InformationForGameManager").GetComponent<InformationFromMenuToGameManager>();
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllPlayers();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].instance =
                Instantiate(playerPrefab, players[i].spawnPoint.transform.position, players[i].spawnPoint.transform.rotation) as GameObject;
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
        cameraControl.ResetCamera();
        ResetAllPlayers();
        ResetAllRamps();
        ResetAllStagePieces();
        DisablePlayerControl();

        roundNumber++;
        messageText.text = "Round " + roundNumber;

        yield return startWait;
    }


    private IEnumerator RoundPlaying()
    {
        MakeAllPlayersKinimetic();
        StartCoroutine(WaitToEnablePlayers());

        messageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }

    IEnumerator WaitToEnablePlayers()
    {
        yield return new WaitForSeconds(3f);

        EnablePlayerControl();
    }

    void MakeAllPlayersKinimetic()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].MakeNotKinematic();
        }
    }

    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();

        roundWinner = null;

        roundWinner = GetRoundWinner();

        if (roundWinner != null)
            roundWinner.wins++;

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;


        yield return endWait;    }


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

    void LockPlayersYAxis()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].LockYAxis();
        }
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


    private void ResetAllPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }

    private void ResetAllRamps()
    {
        for (int i = 0; i < players.Length; i++)
        {
            allRamps[i].Reset();
        }
    }
    private void ResetAllStagePieces()
    {
        for (int i = 0; i < players.Length; i++)
        {
            allStagePieces[i].Reset();
        }
    }

    private void EnablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }


    private void DisablePlayerControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }
}
