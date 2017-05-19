using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

    public class GameManager : MonoBehaviour
    {
        public int m_NumRoundsToWin = 3;
        public float m_StartDelay = 3f;
        public float m_ResetDelay = 5f;
        float countDownTime = 4f;
        public float m_EndDelay = 5f;
        public CameraControl m_CameraControl;
        public Text m_MessageText;
        public GameObject m_TankPrefab;

        [SerializeField]
        public PlayerManager[] m_Tanks;

        public TimeLimit timeLimit;
       
    [SerializeField]
        GameObject[] startingPlatforms;
  
    [SerializeField]
        GameObject goalSphereToggle;
    [SerializeField]
    GameObject waterPlane;


        private int m_RoundNumber = 1;
        private WaitForSeconds m_StartWait;
        private WaitForSeconds m_EndWait;
        private WaitForSeconds m_ResetTime;
    //The scripts are aligned with player manager not tank manager...I think.
        private PlayerManager m_RoundWinner;
        private PlayerManager m_GameWinner;
          
        private void Start()
        {
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);
        m_ResetTime = new WaitForSeconds(m_ResetDelay);

        ShowStartingPlatforms();
        SpawnAllTanks();
            SetCameraTargets();
            StartCoroutine(GameLoop());
        }
       
        private void SpawnAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].m_Instance = Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
                m_Tanks[i].m_PlayerNumber = i + 1;
                m_Tanks[i].Setup();
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
                SceneManager.LoadScene(0);
            }
            else
            {
                StartCoroutine(GameLoop());
            }
        }

        private IEnumerator RoundStarting()
        {
            goalSphereToggle.SetActive(false);
            //Debug.Log("Round Starting");
            ShowStartingPlatforms();
            ResetAllTanks();
            DisableTankControl();
            m_CameraControl.SetStartPositionAndSize();
        for (int i = 0; i < startingPlatforms.Length; i++)
        {
            if (i != startingPlatforms.Length)
            {
                yield return new WaitForSeconds(1);
            }
        }                         
         //goalSphereToggle && waterPlane.transform.position.y <= 1.5            
            yield return StartCoroutine(StartCountDown());

                m_RoundNumber++;
                if (m_StartDelay == 0)
                    {
                         m_MessageText.text = "ROUND" + m_NumRoundsToWin + " Start!";
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
             waterPlane.transform.position = Vector3.zero;
            EnableTankControl();
            HideStartingPlatforms();
            
        if (waterPlane.transform.position.y <= 1.5)
        {
            
            EnableTankControl();
            HideStartingPlatforms();
        }
        else
        {
            WaitAGodDamnSecond();
            Debug.Log("Round Stalling");
        }
        m_MessageText.text = string.Empty;        
            while (!OneTankLeft())
            {
                yield return null;
            }          
        }
        private IEnumerator RoundEnding()
        {
                        //+Debug.Log("Round ended");
            DisableTankControl();
            m_RoundWinner = null; //Original roundwinner placement
            m_RoundWinner = GetRoundWinner();

            if (m_RoundWinner != null)
            m_RoundWinner.m_Wins++;
       // goalSphereToggle.SetActive(false);
      //  m_RoundWinner = null; //I think this should be after...I get roundwinner
            m_GameWinner = GetGameWinner();
             string message = EndMessage();
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
        private PlayerManager GetRoundWinner()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance.activeSelf)
                    return m_Tanks[i];
        }           
            return null;
        }
        private PlayerManager GetGameWinner()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                    return m_Tanks[i]; //  return m_Tanks[i];
        }
            return null;
        }
        private string EndMessage()
        {
          string message = "DRAW!";        
            if (m_RoundWinner == null)
                 {
                       message = "Time UP!" + "\n\n\n" + "DRAW!";
                 }
        if (m_RoundWinner != null)
        {
            return message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";
            return message += "\n\n\n\n";
        }           
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                return message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
            }

        if (m_GameWinner != null)
        {
            return message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";
        }
            
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
        private void HideStartingPlatforms()
        {
            for (int i = 0; i < startingPlatforms.Length; i++)
            {
                startingPlatforms[i].SetActive(false);
             }         
        }
    private void ShowStartingPlatforms()
    {
        for (int i = 0; i < startingPlatforms.Length; i++)
        {
            startingPlatforms[i].SetActive(true);
        }
    }
    private void DisableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].DisableControl();
            }
        } 
}