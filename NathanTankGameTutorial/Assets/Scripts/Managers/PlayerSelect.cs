using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelect : SingletonPlayerSelect
{
    [SerializeField] GameObject TankPrefab;
    public TankManager[] PlayerTanksToEnter;

    List<TankManager> currentPlayers = new List<TankManager>(); //the players currently in play
    public int NumberOfPlayers { get { return currentPlayers.Count; } } //the number of players currently in play

    List<string> playerEntryButton = new List<string>();    //used to check if Fire one button is pressed
    List<string> altPlayerEntryButton = new List<string>(); //used to check if Fire two button is pressed

    // Use this for initialization
    void Start()
    {
        for (int x = 1; x <= PlayerTanksToEnter.Length; x++)
        {
            playerEntryButton.Add("Fire1_P" + x);
            altPlayerEntryButton.Add("Fire2_P" + x);
            //Debug.Log("Player " + x + " entry buttons: " + playerEntryButton[x-1] + " or " + altPlayerEntryButton[x-1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0;x < PlayerTanksToEnter.Length; x++)
        {
            //if the player has pressed an entry button (either Fire 1 or Fire 2)...
            if ((Input.GetButtonDown(playerEntryButton[x]) || Input.GetButtonDown(altPlayerEntryButton[x])) 
                && PlayerTanksToEnter[x] != null) //AND if the current tank has not been spawned... (not null)
            {
                //Debug.Log("Pressed: " + playerEntryButton[x]);
                PlayerTanksToEnter[x].m_Instance =
                Instantiate(TankPrefab, PlayerTanksToEnter[x].m_SpawnPoint.position, PlayerTanksToEnter[x].m_SpawnPoint.rotation) as GameObject;
                PlayerTanksToEnter[x].m_PlayerNumber = x + 1;
                PlayerTanksToEnter[x].Setup();

                //add the player to the list of currently active players
                currentPlayers.Add(PlayerTanksToEnter[x]);

                //remove this player from potential tanks to spawn (so we can't spawn multiple tanks for the same player)
                PlayerTanksToEnter[x] = null;
            }
        }
    }
}