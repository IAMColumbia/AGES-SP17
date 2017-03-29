using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelect : SingletonPlayerSelect
{
    [SerializeField] GameObject TankPrefab;
    public TankManager[] PlayerTanksToEnter;


    //player counting
    List<GameObject> currentPlayers = new List<GameObject>(); //the players currently in play
    public int NumberOfPlayers { get { return currentPlayers.Count; } } //the number of players currently in play
    public List<GameObject> CurrentPlayers { get { return currentPlayers; } }

    //input buttons
    List<string> playerEntryButton = new List<string>();    //used to check if Fire one button is pressed
    List<string> altPlayerEntryButton = new List<string>(); //used to check if Fire two button is pressed


    // Use this for initialization
    new void Start()
    {
        base.Start();

        for (int x = 1; x <= PlayerTanksToEnter.Length; x++)
        {
            ShellExplosion.DestroyAllBullets = false;

            playerEntryButton.Add("Fire1_P" + x);
            altPlayerEntryButton.Add("Fire2_P" + x);
            //Debug.Log("Player " + x + " entry buttons: " + playerEntryButton[x-1] + " or " + altPlayerEntryButton[x-1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RemovePlayerWhenInactive();
        CheckForPlayerEntry();
    }

    private void CheckForPlayerEntry()
    {
        for (int x = 0; x < PlayerTanksToEnter.Length; x++)
        {
            //if the player has pressed an entry button (either Fire 1 or Fire 2)...
            if ((Input.GetButtonDown(playerEntryButton[x]) || Input.GetButtonDown(altPlayerEntryButton[x]))
                && !currentPlayers.Contains(PlayerTanksToEnter[x].m_Instance)) //AND if the current tank has not been spawned... (not null)
            {
                //Debug.Log("Pressed: " + playerEntryButton[x]);
                PlayerTanksToEnter[x].m_Instance =
                Instantiate(TankPrefab, PlayerTanksToEnter[x].m_SpawnPoint.position, PlayerTanksToEnter[x].m_SpawnPoint.rotation) as GameObject;
                PlayerTanksToEnter[x].m_PlayerNumber = x + 1;
                PlayerTanksToEnter[x].Setup();

                //prevent the player from firing or sucking
                //PlayerTanksToEnter[x].DisableShootingOnly();

                //add the player to the list of currently active players
                currentPlayers.Add(PlayerTanksToEnter[x].m_Instance);
                DontDestroyOnLoad(PlayerTanksToEnter[x].m_Instance);
            }
        }
    }

    private void RemovePlayerWhenInactive()
    {
        for(int x = currentPlayers.Count - 1; x >= 0 && currentPlayers.Count > 0; x--)
        {
            if (currentPlayers[x].activeSelf == false)
            {
                GameObject playerToRemove = currentPlayers[x];
                currentPlayers.Remove(playerToRemove);
                Destroy(playerToRemove);
            }
        }
    }
}