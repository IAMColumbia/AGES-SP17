using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

   
    [SerializeField]
    int killCap;

    [SerializeField]
    GameObject PlayerPrefab;

    [SerializeField]
    float StartDelay = 3f;

    [SerializeField]
    float SpawnDelay = 2f;

    [SerializeField]
    float EndDelay = 3f;

    [SerializeField]
    float RespawnDelay = 4f;

    [SerializeField]
    GameObject afterActionReport;

    public CameraScript CameraControl;
    public PlayerManager[] Players;
    WaitForSeconds RespawnWait;
    WaitForSeconds StartWait;
    WaitForSeconds GameOverWait;
    WaitForSeconds SpawnWait;
    PlayerManager MatchWinner;
    bool killCapHasBeenReached;
    

     

    // Use this for initialization
    void Start ()
    {
        //afterActionReport.SetActive(false);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level"));
        StartWait = new WaitForSeconds(StartDelay);
        GameOverWait = new WaitForSeconds(EndDelay);

        
        SpawnPlayers();
        SetCameraTargets();

        StartCoroutine(GameLoop());
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        
    }
    void SpawnPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Instance =
                Instantiate(PlayerPrefab, Players[i].SpawnZone.position, Players[i].SpawnZone.rotation) as GameObject;
            //SetSpawnPoints();
            Players[i].PlayerNumber = i + 1;
            Players[i].Setup();
        }
    }
    void SetCameraTargets()
    {
        Transform[] targets = new Transform[Players.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            
            targets[i] = Players[i].Instance.transform;
        }

        CameraControl.Targets = targets;
    }
    IEnumerator GameLoop()
    {
        yield return StartCoroutine(MatchStarting());

        yield return StartCoroutine(MatchPlaying());

        yield return StartCoroutine(MatchEnding());

        if(MatchWinner != null)
        {
            afterActionReport.SetActive(true);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    IEnumerator MatchStarting()
    {
        ResetAllPlayers();
        DisablePlayerControl();
        
        yield return StartWait;
    }

    IEnumerator MatchPlaying()
    {
        
        EnablePlayerControl();
       
        while (!KillCountReached())
        {
            KillCountUpdate();
            RespawnTime();
            yield return null;
        }
        Debug.Log("KillCountHasBeenReached");
    }

    IEnumerator MatchEnding()
    {
        DisablePlayerControl();

        MatchWinner = null;

        MatchWinner = GetMatchWinner();

        afterActionReport.GetComponent<AfterActionScript>().winnerText.text = MatchWinner.Instance.GetComponent<PlayerMagic>().name;
        yield return GameOverWait;
    }

    private bool KillCountReached()
    {
        
        for (int i = 0; i < Players.Length; i++)
        {
            if(Players[i].KillCount == killCap)
            {
                killCapHasBeenReached = true;
            }
        }
        return killCapHasBeenReached;
    }

    void ResetAllPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Reset();
        }
    }

    PlayerManager GetMatchWinner()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].KillCount == killCap)
            {
                return Players[i];
            }
        }
        return null;
    }

    void DisablePlayerControl()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].DisableCharacterControl();
        }
    }

    void EnablePlayerControl()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].EnableCharacterControl();
        }
    }

    void SetSpawnPoints()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            int x = i + 1;
            string num = x.ToString();
            
            Players[i].SpawnZone = GameObject.Find("PlayerSpawn" + num).transform;
        }
    }
   

    void RespawnTime()
    {
        
        for (int i = 0; i < Players.Length; i++)
        {
            
            Players[i].Respawn();

        }
        
    }
    void KillCountUpdate()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].KillCount = Players[i].MagicScript.GetComponent<PlayerMagic>().KillCount;
        }
    }

   
}
