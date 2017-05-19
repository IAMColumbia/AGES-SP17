using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public class GoalSphere : MonoBehaviour
{

    // Use this for initialization
    bool shouldDisableWhenDonePlayingSoundEffect = false;
    public AudioSource audioSource;

    [SerializeField]
    GameObject goalSphereToggle;
    GameManager gameManager;
    [SerializeField]
    GameObject centerPlatform;
    [SerializeField]
    GameObject[] waterSpray;
    Vector3 centerPlatformPosition;
    [SerializeField]
    Vector3 winnerPlatform;
    PlayerManager[] m_Tanks;
    public bool HasGoal
    {
        get
        {
            return goalSphereToggle.activeSelf;
        }
    }
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        goalSphereToggle.SetActive(false);    
    }
    void Update()
    {
        Rotation();
        checkGoalSphereToggle();
    }

    private void checkGoalSphereToggle()
    {
        if (HasGoal)
        {
            Debug.Log("goalSphereToggle.SetActive(False)Drown other players!");
            ShowWaterParticles();           
        }
        else if (!HasGoal)
        {
            HideWaterParticles();
        }
    }

    void Rotation()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
        if (shouldDisableWhenDonePlayingSoundEffect)
        {
            transform.Rotate(25, 300 * Time.deltaTime, 100);          
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
            
        }
    }   
    void HideWaterParticles()
    {
        for (int i = 0; i < waterSpray.Length; i++)
        {
            waterSpray[i].SetActive(false);
            audioSource.Stop();
        }
    }
    void ShowWaterParticles()
    {
        for (int i = 0; i < waterSpray.Length; i++)
        {
            waterSpray[i].SetActive(true);
            audioSource.Play();
        }
    }
}
   




