using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
//using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour
{
   
    public int m_NumRoundsToWin = 3;
    public float m_StartDelay = 3f;
    public float m_ResetDelay = 5f;
    float countDownTime = 4f;
    public float m_EndDelay = 5f;

    PlayerFlightControl playerFlightControl;
    [SerializeField]
    GameObject textBox;
    public Text m_MessageText;
    public GameObject ringToggle;

    [SerializeField]
    public RingManager[] ringSpawnPoints;

    [SerializeField]
    GameObject[] rings;
    [SerializeField]
    GameObject ring;
    int ringIndex;
  
    public TimeLimit timeLimit;
    Text totalText;
  
    [SerializeField]
    ArrayList[] ringSets;

    [SerializeField]
    GameObject m_RoundWinner;
    [SerializeField]
    GameObject m_GameWinner;
    Text countText;
    public bool HasGoal
    {
        get
        {
            return m_RoundWinner.activeSelf;
        }
    } 
    private int m_RoundNumber = 1;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private WaitForSeconds m_ResetTime;
    //The scripts are aligned with player manager not tank manager...I think.        
    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        m_ResetTime = new WaitForSeconds(m_ResetDelay);

       ring = GameObject.FindGameObjectWithTag("Ring");
       rings = GameObject.FindGameObjectsWithTag("Rings");
       totalText = GameObject.Find("Total Rings").GetComponent<Text>();
       countText = GameObject.Find("Rings Collected").GetComponent<Text>();
        //ShowRings();

        SpawnAllRings();
        textBox.SetActive(false);
        StartCoroutine(GameLoop());
        
    }

    private void SpawnAllRings()
    {
        for (int i = 0; i < ringSpawnPoints.Length; i++)
        {
            ringSpawnPoints[i].m_Instance = Instantiate(ringToggle, ringSpawnPoints[i].m_SpawnPoint.position, ringSpawnPoints[i].m_SpawnPoint.rotation) as GameObject;
            ringSpawnPoints[i].m_RingNumber = i + 1;

            ringSpawnPoints[i].Setup();
        }
    }
    private IEnumerator GameLoop()
    {
        
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        StartCoroutine(GameLoop());
        if (m_GameWinner != null)
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
        totalText.text = ringSpawnPoints.Length.ToString();
        GameObject m_RoundWinner;
        m_RoundWinner = GameObject.FindGameObjectWithTag("RoundWinner");
     
        m_RoundWinner.SetActive(false);
       ResetRings();//SpawnAllRings use to be here, now ResetRings placement

        for (int i = 0; i < ringSpawnPoints.Length; i++)
        {
            if (i != ringSpawnPoints.Length)
            {
                yield return new WaitForSeconds(1);
            }
        }
        yield return StartCoroutine(StartCountDown());
        m_RoundNumber++;
        if (m_StartDelay == 0)
        {
            m_MessageText.text = "ROUND" + m_RoundNumber + " Start!";
        }
        yield return m_StartWait;
    }
    private IEnumerator WaitAGodDamnSecond()
    {
        yield return m_ResetDelay;
    }
    public IEnumerator StartCountDown()
    {
        while (countDownTime > 0f)
        {
            textBox.SetActive(true);
            countDownTime -= 1f;
            yield return new WaitForSeconds(1f);
            m_MessageText.text = countDownTime.ToString();
        }
        if (countDownTime == 0)
        {
            m_MessageText.text = "Round " + m_RoundNumber + " Start!";
        }
    }
    private IEnumerator RoundPlaying()
    {
        Debug.Log("RoundPlaying");
        textBox.SetActive(false);
      
        m_MessageText.text = string.Empty;
        while (!noRingsActive())
        {
            yield return null;
            Debug.Log("while loops no rings active");
            yield return null;
        }    
    }

    private bool noRingsActive()
    {
        //int numRingsCollected = 0;
        int ringsNeeded = 3;
        ring = rings[0]
        foreach (GameObject ring in rings)
        {
            ringsNeeded--;
            playerFlightControl.ringCount = ringIndex;

            Debug.Log("RingsNeeded: " + ringsNeeded);
            Debug.Log("playerFlightControl.ringCount: " + playerFlightControl.ringCount);
            Debug.Log("ringIndex: " + ringIndex);
        }
        Debug.Log("foreachLoop");
        return ringsNeeded <= 0;
    }

    //private bool OneTankLeft()
    //{
    //    int numTanksLeft = 0;

    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        if (m_Tanks[i].m_Instance.activeSelf)
    //            numTanksLeft++;
    //    }
    //    return numTanksLeft <= 1;
    //}


    private IEnumerator RoundEnding()
    {
    textBox.SetActive(true);
    m_RoundWinner.SetActive(true);
        Debug.Log("Round ended");       
        m_RoundWinner = null; //Original roundwinner placement                 

        //     HasGoal = GetGameWinner();
       
        m_MessageText.text = "Round "+ (m_RoundNumber - 1)+" Done, Good Job!";
     
        string message = EndMessage();
        yield return m_EndWait;
    }
   
    private void GetGameWinner()
    {
     
       
    }
 
    
    private string EndMessage()
    {
        textBox.SetActive(true);
        string message = "Better luck next time!";
           
        if (m_RoundWinner != null)
        {
            return message = " Good job!";
        }
        if (HasGoal)
        {
            return message = "You Win!!";
        }
        return message;
    }
    private void ResetRings()
    {
        for (int i = 0; i < ringSpawnPoints.Length; i++)
        {
            ringSpawnPoints[i].Reset();
        }
    }
}