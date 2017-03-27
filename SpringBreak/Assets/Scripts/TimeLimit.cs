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

    Hazard hazard = new Hazard();

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
    float timeLeft = 60;

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
      
        StartMatch();
    }

    private void StartMatch()
    {
       
    }

    void Update()
    {

       // TimeLeft + timeAdded;
       
        TimeLeft -= Time.deltaTime;
        timeText.text = ((int)TimeLeft).ToString();

        // Debug.Log("Time Left: " + timeLeft);
        if (TimeLeft < 0)
        {
            Debug.Log("Time UP!");
            GameOver();
        }
        if(TimeLeft <= 30)
        {
            hazard.WaterRise();
            m_MessageText.text = "Hurry up!";
        }
    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }
}
