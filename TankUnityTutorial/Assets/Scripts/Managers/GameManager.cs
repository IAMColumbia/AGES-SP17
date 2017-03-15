﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    float secondsTillBlockSpawns;

    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    private TankHealth tankParticles;       

    public int m_NumRoundsToWin = 5;        
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public CameraControl m_CameraControl;   
    public Text m_MessageText;       
           
    public GameObject m_TankPrefab;         
    public TankManager[] m_Tanks;

    public GameObject blockPrefab;
    public BlockManager[] blocks;         


    private int m_RoundNumber;              
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;
    private WaitForSeconds blockSpawn;       
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;


    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        blockSpawn = new WaitForSeconds(secondsTillBlockSpawns);

        SpawnAllTanks();
        SpawnBlocks();
        SetCameraTargets();

        StartCoroutine(GameLoop());      
    }


    private void SpawnAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance =
                Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();
        }
    }

    private void SpawnBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
           blocks[i].blockInstance = 
                Instantiate(blockPrefab, blocks[i].blockSpawnPoint.position, blocks[i].blockSpawnPoint.rotation) as GameObject;            
        }
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
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
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        ResetAllBlocks();
        DisableTankControl();
        DisableBlocks();
        DisableParticleSystems();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl();
        EnableBlocks();

        m_MessageText.text = string.Empty;


        while (!OneTankLeft())
        {            
            SpawnBlocks();
            yield return blockSpawn;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();
        DisableBlocks();

        m_RoundWinner = null;

        m_RoundWinner = GetRoundWinner();

        if(m_RoundWinner != null)
        {
            m_RoundWinner.m_Wins++;
        }

        m_GameWinner = GetGameWinner();

        string message = EndMessage();
        m_MessageText.text = message;

        yield return m_EndWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }


    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return m_Tanks[i];
        }

        return null;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return m_Tanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }
    }

    private void ResetAllBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].ResetBlocks();
        }
    }

    private void EnableBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].EnableSpawner();
        }
    }

    private void DisableBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].DisableSpawner();
        }
    }

    private void DisableParticleSystems()
    {
        tankParticles.tankParticles.gameObject.SetActive(false);
    }
}