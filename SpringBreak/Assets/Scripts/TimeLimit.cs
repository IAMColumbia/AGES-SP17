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
    float waterSpeed = 0.5f;

    [SerializeField]
    GameObject waterPlane;

    [SerializeField]
    float itemSpawnHeight = 15;

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
        
        StartMatch();
    }

    private void StartMatch()
    {
        WaterDescend();
    }

    void Update()
    {

        // TimeLeft + timeAdded;

        TimeLeft -= Time.deltaTime;
        timeText.text = ((int)TimeLeft).ToString();

        // Debug.Log("Time Left: " + timeLeft);
        if (TimeLeft < 0)
        {
           
            
        }
        if (TimeLeft <= 50)
        {
            WaterRise();
            m_MessageText.text = "Hurry up!";
        }
        if (TimeLeft == 80)
        {
            SpawnItems();
        }
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

    public void WaterRise()
    {
        waterSpeed = 0.5f;
        waterPlane.transform.Translate(Vector3.up * waterSpeed * Time.deltaTime, Space.World);

    }
    public void WaterDescend()
    {
        waterSpeed = 1.5f;
        waterPlane.transform.Translate(Vector3.down * waterSpeed * Time.deltaTime, Space.World);

    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }
}
