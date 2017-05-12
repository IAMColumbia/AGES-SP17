using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
//using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour
{
   
    public int m_NumLapsToWin = 3;
    public float m_StartDelay = .01f;
    public float m_ResetDelay = .5f;
    float countDownTime = 4f;
    public float m_EndDelay = 0.01f;

   
    
    [SerializeField]
    public GameObject textBox;
    public Text m_MessageText;
    public GameObject ringToggle;

    [SerializeField]
    public RingManager[] ringSpawnPoints;

    [SerializeField]
    GameObject[] rings;

    //public GameObject roundWonPlane;
   
    int ringIndex;
  
    public TimeLimit timeLimit;
    Text totalText;
    //[SerializeField]
    bool roundWon;
    [SerializeField]
    public GameObject round1;
    [SerializeField]
    public GameObject round2;
    [SerializeField]
    public GameObject round3;
    public bool roundOneDone = false;
    public bool roundTwoDone = false;
    public bool roundThreeDone = false;
    public GameObject m_RoundWinner;
    [SerializeField]
    public GameObject m_GameWinner;
    Text countText;
    private int m_NumOfLaps = 1;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private WaitForSeconds m_ResetTime;

    public bool Round1
    {
        get
        {
            return round1.activeSelf;
        }
    }
    public bool Round2
    {
        get
        {
            return round2.activeSelf;
        }
    }
    public bool Round3
    {
        get
        {
            return round3.activeSelf;
        }
    }





    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        m_ResetTime = new WaitForSeconds(m_ResetDelay);
  
       totalText = GameObject.Find("Total Rings").GetComponent<Text>();
          
        m_RoundWinner = GameObject.FindGameObjectWithTag("RoundWinner");
        //roundWonPlane = GameObject.FindGameObjectWithTag("Finish");
        rings = GameObject.FindGameObjectsWithTag("Ring Trigger");
      
      
   //     SpawnAllRings();
        textBox.SetActive(false);
        StartCoroutine(GameLoop());
        m_GameWinner.SetActive(false);

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
        if (m_GameWinner.activeSelf)
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
  
        m_RoundWinner = GameObject.FindGameObjectWithTag("RoundWinner");
        m_RoundWinner.transform.Translate(Vector3.up);
        roundWon = false;
        // m_RoundWinner.SetActive(false);
        SpawnAllRings();

        for (int i = 0; i < ringSpawnPoints.Length; i++)
        {
            if (i != ringSpawnPoints.Length)
            {
                yield return new WaitForSeconds(1);
            }
        }
        yield return StartCoroutine(StartCountDown());
       
        if (m_StartDelay == 0)
        {
      //    m_MessageText.text = "ROUND" + m_NumOfLaps + " Start!";
        }
        yield return m_StartWait;
    }
    //private IEnumerator WaitAGodDamnSecond()
    //{
    //    yield return m_ResetDelay;
    //}
    public IEnumerator StartCountDown()
    {
        while (countDownTime > 0f)
        {
            textBox.SetActive(true);
            countDownTime -= 1f;
            yield return new WaitForSeconds(0.1f);
            m_MessageText.text = countDownTime.ToString();
        }
        if (countDownTime == 0)
        {
            m_MessageText.text = "Cycle " + m_NumOfLaps + " commence!";
        }
    }
    private IEnumerator RoundPlaying()
    {
        Debug.Log("RoundPlaying");
        textBox.SetActive(false);
      
        m_MessageText.text = string.Empty;
        while (!checkRoundWinner())
        {
            yield return null;
            Debug.Log("while loops no rings active");
           // yield return null;
        }
        Debug.Log("noringsActive now");
    }

    private bool checkRoundWinner()
    {     
        int ringsNeeded = 0;

        if (round3 == null)//if setactive(false) checkRoundWinner(true)
        {
            roundWon = true;
            return roundWon;
        }          
       if(round3 != null)
        {
            if (Round3 == false)
            {
                return roundWon;
            }
            if (round2 == null)//if setactive(false) checkRoundWinner(false)
                return false;
            if (Round2 == false)
            {
                roundWon = true;
                return roundWon;
            }
            if (round2 != null)
            {
                if (round1 == null)
                    return false;
                if (Round1 == false)
                {
                    roundWon = true;
                    return roundWon;
                }
            }           
        }      
      
       return ringsNeeded >= 30;
    }

    private IEnumerator RoundEnding()
    {
    textBox.SetActive(true);
    //m_RoundWinner.SetActive(true);
        Debug.Log("Round ended");

        m_MessageText.text = "Cycle " + (m_NumOfLaps) + " complete...";
        string message = EndMessage();
        yield return m_EndWait;
        m_RoundWinner = null; //Original roundwinner placement                 

        //     HasGoal = GetGameWinner();
        if (m_GameWinner != null)
            m_NumOfLaps++;
        if(m_NumOfLaps > 3)
        {
            m_GameWinner.SetActive(true);
        }     
        yield return m_EndWait;
    }  
    private string EndMessage()
    {
        textBox.SetActive(true);
        string message = "Better luck next time!";
           
        if (m_RoundWinner != null)
        {
            return message = " Good job!";
        }
        if (m_GameWinner.activeSelf)
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