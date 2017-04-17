using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public int ButtsToBlast = 15;
    public float StartDelay = 5f;
    public float EndDelay = 5f;
    public GameObject[] PlayerPrefabs;
    public PlayerManager[] Players;

    private CameraControl CameraControl;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager gameWinner;

	
	void Start () {
        startWait = new WaitForSeconds(StartDelay);
        endWait = new WaitForSeconds(EndDelay);

        CameraControl = GameObject.FindObjectOfType<CameraControl>();

        SpawnAllPlayers();
        SetCameraTargets();
	}

    private void SpawnAllPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Instance = Instantiate(PlayerPrefabs[i], Players[i].SpawnPoint.position, Players[i].SpawnPoint.rotation) as GameObject;
            Players[i].PlayerNumber = i + 1;
            Players[i].Setup();
            
        }
    }

    public void RespawnPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if(!Players[i].Instance.GetComponent<Rigidbody2D>().isKinematic == true)
            {
                Players[i].Instance.transform.position = Players[i].SpawnPoint.position;
                Players[i].PlayerNumber = i + 1;
                Players[i].Reset();
                SetCameraTargets();
            }
        }
    }
    

    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].ButtsBlasted == ButtsToBlast)
                return Players[i];
        }

        return null;
    }

    public void StoreButtsBlasted()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].ButtsBlasted++;
        }
    }

    private void SetCameraTargets()
    {
        Transform[] players = new Transform[Players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = Players[i].Instance.transform;
        }

        CameraControl.Targets = players;
    }
    
}
