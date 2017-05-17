using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour {

    // Use this for initialization
   
    [SerializeField]
    GameObject timeLimitPanel;

    [SerializeField]
    Text timeText;
    [SerializeField]
    Text m_MessageText;

    [SerializeField]
    GameObject roundWinner;

    [SerializeField]
    GameObject timeUpToggle;
    
    public float TimeLeft
    {
        get
        {
            return timeLeft;
        }
        set
        {
            timeLeft = value;
        }
    }
    public bool HasGoal
    {
        get
        {                  
            return timeUpToggle.activeSelf;

        }

    }
    float timeLeft = 99;
    float timeAdded;
    public void TimeUP()
    {
        timeAdded = UnityEngine.Random.Range(5, 20);
        timeLeft += timeAdded;
        Debug.Log("Current Time: " + timeLeft + "\n" + "Time Added: " + timeAdded);
        
    } 
    void start()
    {      
        timeText = GetComponent<UnityEngine.UI.Text>();
        timeUpToggle.SetActive(false);            
    }   
    void Update()
    {      
        TimeLeft -= Time.deltaTime;
        timeText.text = ((int)TimeLeft).ToString();

        // Debug.Log("Time Left: " + timeLeft);
        if(TimeLeft >= 100)
        {
            timeText.text = "∞";
        }
        if (TimeLeft <= 30)
        {
            float warningMessageInterval = 2.5f;
          
            warningMessageInterval -= Time.deltaTime;
            m_MessageText.text = "Hurry up!";
            if (warningMessageInterval <= 0)
            {
             m_MessageText.text = "";
            }        
        }
        checkWinner();
       
        if (HasGoal)
        {
            TimeLeft = 75;
        }
        if(TimeLeft <= 0)
        {
            GameOver();
        }
    }

    private void checkWinner()
    {
        if (roundWinner.activeSelf)
        {
            timeUpToggle.SetActive(true);
        }
        else if (!roundWinner.activeSelf)
        {
            timeUpToggle.SetActive(false);
        }
    }

    private void GameOver()
    {
        m_MessageText.text = "Time Up!";
        
    }
}
