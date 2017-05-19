﻿using UnityEngine;
using System.Collections;
//using UnityStandardAssets.Characters.ThirdPerson;
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
    Vector3 winnerPlatform;
   RingManager[] rings;
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
                 
        }
        else if (!HasGoal)
        {
          
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
            other.gameObject.transform.position = winnerPlatform;
            
        }
    }   
   
}
   




