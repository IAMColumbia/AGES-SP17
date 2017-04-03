using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public class GoalSphere : MonoBehaviour
{

    // Use this for initialization
    bool shouldDisableWhenDonePlayingSoundEffect = false;
    public AudioSource audioSource;
    float duration = 3;

    [SerializeField]
    GameObject goalSphereToggle;
    GameManager gameManager;
    [SerializeField]
    GameObject centerPlatform;
    [SerializeField]
    GameObject[] waterSpray;
    Vector3 centerPlatformPosition;
    //CoRoutine Variables
    float m_SteadyDelay = .1f;
    float m_StartDelay = 1f;      
    float m_EndDelay = 5f;
    private WaitForSeconds m_EndWait;

    [SerializeField]
    Vector3 winnerPlatform;
    PlayerManager[] m_Tanks;   
    public float waterSpeed = 10f;
    [SerializeField]
    public GameObject waterPlane;
    //(center.transform.position.x, center.transform.position.y, transform.position.z);
    void Start()
    {
       AudioSource audioSource = GetComponent<AudioSource>();
        goalSphereToggle.SetActive(false);
        
        m_EndWait = new WaitForSeconds(m_EndDelay);
    }
    void Update()
    {
        Rotation();
        CheckforGoalSphere();
    }

    private void CheckforGoalSphere()
    {
        if (goalSphereToggle && waterPlane.transform.position.y >= 10)
        {
            waterPlane.transform.position = Vector3.zero;
        }
    }

    void Rotation()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
        if (shouldDisableWhenDonePlayingSoundEffect)
        {
            transform.Rotate(25, 300 * Time.deltaTime, 100);
            FadeOut();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched Sphere");
            audioSource.Play();
            goalSphereToggle.SetActive(true);
            centerPlatform.SetActive(true);
            other.gameObject.transform.position = winnerPlatform;
            //Start Reset Game Coroutine
            StartCoroutine(ResetGame());                    
        }
    }
    IEnumerator ResetGame()
    {
        yield return StartCoroutine(WaterRise());
      //  yield return StartCoroutine(WaterRescind());
    }
    void FadeOut()
    {
        if (shouldDisableWhenDonePlayingSoundEffect && !audioSource.isPlaying)
        {
            float t;
            float alpha = GetComponent<MeshRenderer>().material.color.a;
            for (t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0, t));
                GetComponent<MeshRenderer>().material.color = newColor;
            }
        }
    }
    IEnumerator WaterRise()
    {
        Debug.Log("Water Rise");
        if (goalSphereToggle)
        {            
            Debug.Log("goalSphereToggle.SetActive(False)Drown other players!");
            ShowWaterParticles();
            while (goalSphereToggle)
            {
                if (waterPlane.transform.position.y < centerPlatform.transform.position.y)
                {
                    for (int i = 0; i < 220; i++)
                    {
                        InvokeRepeating("SlowRise", m_SteadyDelay, i);
                        if (waterPlane.transform.position.y == centerPlatform.transform.position.y || !goalSphereToggle)
                        {
                        //    WaterRescind();
                            break;                                                      
                        }
                    }                 
                }
                if (!goalSphereToggle)
                    audioSource.Stop();
                   WaterRescind();
                yield return m_EndWait;
                break;            
            }           
        }
    }
    void SlowRise()
    {
        waterPlane.transform.position += Vector3.up *waterSpeed * Time.deltaTime;
    }
    private void HideWaterParticles()
    {
        for (int i = 0; i < waterSpray.Length; i++)
        {
            waterSpray[i].SetActive(false);
        }

    }
    private void ShowWaterParticles()
    {
        for (int i = 0; i < waterSpray.Length; i++)
        {
            waterSpray[i].SetActive(true);
        }
    }
    IEnumerator WaterRescind()
    {
      if (!goalSphereToggle)
        {
            //yield return new WaitForSeconds(3);
           HideWaterParticles();
           Debug.Log("goalSphereToggle.SetActive(True)Reset the Stage Hazard!");
           while (!goalSphereToggle)
           {
              if (waterPlane.transform.position.y >= 1)
               {
                    for (int i = 0; i < 1000; i++)
                   {
                       InvokeRepeating("SlowRescind", m_SteadyDelay, i);
                        if (waterPlane.transform.position.y == 0)
                      {
                           goalSphereToggle.SetActive(false);
                          break;                           
                          //  yield return new WaitForSeconds(3);
                    }
                }

             }
           if(goalSphereToggle)
               yield return m_EndWait;
          break;
           }          
      }
    }
    void SlowRescind()
   {
       waterPlane.transform.position += Vector3.down * Time.deltaTime;
   }
}

