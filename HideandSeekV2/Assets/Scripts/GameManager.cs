using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject m_PlayerPrefab;             // Reference to the prefab the players will control.
    public PlayerManager[] m_Players;               // A collection of managers for enabling and disabling different aspects of the tanks.
    public Camera[] m_PlayerCams;

    [SerializeField] public int RequiredNumHiders = 3, NumOfHiders = 0, NumberRolled;
     [SerializeField] public  bool hasSeeker = false;

    [SerializeField]
    Text GameTimerText;

    [SerializeField]
    float GameTimer;
    
    [SerializeField]
    public AudioClip[] audioClip;
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public int AudioSourcetoPlay;

    [SerializeField]
    GameObject EndGamePanel;
    [SerializeField]
    Text endGameText;


    private void SpawnPlayers()
    {
       
        for (int i = 0; i < m_Players.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            m_Players[i].m_Instance =
                Instantiate(m_PlayerPrefab, m_Players[i].m_SpawnPoint.position, m_Players[i].m_SpawnPoint.rotation) as GameObject;
            m_Players[i].RollANumber();            
            m_Players[i].m_PlayerNumber = i + 1;
            m_Players[i].m_Instance.name = "Player" + m_Players[i].m_PlayerNumber;
            m_Players[i].Setup();
  

        }
    }


    void Start ()
    {
        EndGamePanel.SetActive(false);
        SpawnPlayers();

    }

    void Update ()
    {
        AudioSourcetoPlay = Random.Range(0, audioClip.Length);
        GameTimer -= Time.deltaTime;
         
        GameTimerText.text = Mathf.Round(GameTimer).ToString();
        if (GameTimer <= 0f)
        {

            EndGame();
        }

        if (NumOfHiders == 0 && GameTimer != 0)
        {
            EndGame();

        }
    }

    void EndGame()
    {
        if (NumOfHiders>0)
        {
            endGameText.text = "HIDERS WIN";

        }
        if (NumOfHiders <= 0)
        {
            endGameText.text = "SEEKER WINS";
        }
        EndGamePanel.SetActive(true);
    }

}
