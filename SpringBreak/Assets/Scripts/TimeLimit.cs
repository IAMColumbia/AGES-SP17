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
    GameObject[] weightedObjects;
    
    [SerializeField]
    float itemSpawnHeight = 15;

    [SerializeField]
    GameObject goalSphereToggle;
    [SerializeField]
    GameObject waterPlane;


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
            return goalSphereToggle.activeSelf;

        }

    }
    float timeLeft = 112;

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
      
        if (TimeLeft <= 50)
        {
            float warningMessageInterval = 5;
           
           warningMessageInterval -= Time.deltaTime;
            if(warningMessageInterval <= 0)
            {
             m_MessageText.text = "";
            }
           SlowWaterRise();
           m_MessageText.text = "Hurry up!";
        }
        //if (TimeLeft == 80)
        //{
        //    SpawnItems();
        //}
        if (HasGoal)
        {
            TimeLeft = 99;
        }
    }

    private void SlowWaterRise()
    {
        float waterSpeed;

        waterSpeed = 0.03f;
        waterPlane.transform.Translate(Vector3.up * waterSpeed * Time.deltaTime, Space.World);
        waterPlane.transform.Translate(Vector3.up);
         
        
    }

    private void SpawnItems()
    {

        for (int i = 0; i < weightedObjects.Length; i++)
        {                           
            int r = UnityEngine.Random.Range(0, weightedObjects.Length);
            Vector3 position = new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), itemSpawnHeight, UnityEngine.Random.Range(-10.0f, 10.0f));
            Instantiate(weightedObjects[r], position, Quaternion.identity);
        }                         
    }   
    private void GameOver()
    {
        throw new NotImplementedException();
    }
}
