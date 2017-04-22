using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {
    public GameObject m_PlayerPrefab;             // Reference to the prefab the players will control.
    public PlayerManager[] m_Players;               // A collection of managers for enabling and disabling different aspects of the tanks.
    public Camera[] m_PlayerCams;

    private void SpawnAllTanks()
    {
        // For all the tanks...
        for (int i = 0; i < m_Players.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            m_Players[i].m_Instance =
                Instantiate(m_PlayerPrefab, m_Players[i].m_SpawnPoint.position, m_Players[i].m_SpawnPoint.rotation) as GameObject;
            m_Players[i].m_PlayerNumber = i + 1;
            m_Players[i].m_Instance.name = "Player" + m_Players[i].m_PlayerNumber;
            m_Players[i].Setup();
        }
    }


    void Start ()
    {

        SpawnAllTanks();

    }



}
