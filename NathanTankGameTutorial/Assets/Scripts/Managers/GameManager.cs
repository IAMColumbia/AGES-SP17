using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 3;
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public CameraControl m_CameraControl;   
    public Text m_MessageText;              
    public GameObject m_TankPrefab;         
    public TankManager[] AllTanks;

    private int m_RoundNumber;              
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;       
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;
    private List<TankManager> CurrentTanks = new List<TankManager>();


    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllTanks();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllTanks()
    {
        List<GameObject> players = SingletonPlayerSelect.Instance.CurrentPlayers;

        for (int i = 0; i < AllTanks.Length; i++)
        {
            foreach (GameObject tank in players)
            {
                if (tank.GetComponent<TankMovement>().PlayerNumber - 1 == i)    //causes an error!!! (GameObjects in players list don't exist from the previous scene)
                {
                    AllTanks[i].m_Instance =
                        Instantiate(m_TankPrefab, AllTanks[i].m_SpawnPoint.position, AllTanks[i].m_SpawnPoint.rotation) as GameObject;
                    AllTanks[i].m_PlayerNumber = i + 1;
                    AllTanks[i].Setup();

                    CurrentTanks.Add(AllTanks[i]);
                }
            }
        }

        Destroy(SingletonPlayerSelect.Instance.gameObject);

        foreach (GameObject tank in players)
            Destroy(tank);

    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[CurrentTanks.Count];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = CurrentTanks[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        Debug.Log("Round Starting");
        ResetAllTanks();
        DisableTankControl();

        m_CameraControl.SetStartPositionAndSize();
        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        ShellExplosion.DestroyAllBullets = false;
        Pickup.RespawnAllPickups();

        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl();
        m_MessageText.text = "";

        while(!OneTankLeft())
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();

        m_RoundWinner = null;
        m_RoundWinner = GetRoundWinner();

        if(m_RoundWinner != null)
        {
            m_RoundWinner.m_Wins++;
        }

        m_GameWinner = GetGameWinner();
        string message = EndMessage();
        m_MessageText.text = message;

        ShellExplosion.DestroyAllBullets = true;

        yield return m_EndWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < CurrentTanks.Count; i++)
        {
            if (CurrentTanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < CurrentTanks.Count; i++)
        {
            if (CurrentTanks[i].m_Instance.activeSelf)
                return CurrentTanks[i];
        }

        return null;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < CurrentTanks.Count; i++)
        {
            if (CurrentTanks[i].m_Wins == m_NumRoundsToWin)
                return CurrentTanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < CurrentTanks.Count; i++)
        {
            message += CurrentTanks[i].m_ColoredPlayerText + ": " + CurrentTanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < CurrentTanks.Count; i++)
        {
            CurrentTanks[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < CurrentTanks.Count; i++)
        {
            CurrentTanks[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < CurrentTanks.Count; i++)
        {
            CurrentTanks[i].DisableControl();
        }
    }
}