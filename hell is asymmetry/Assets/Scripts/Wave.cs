using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Wave : MonoBehaviour, Observer, Subject {

    Enemy[] Enemies;

    int numEnemiesInWave;

    [SerializeField]
    Wave previousWave;

    [SerializeField]
    float startDelay = 1;

    int numEnemiesRemaining;

    List<Observer> observers = new List<Observer>();

    public void Notify(Subject sender, Event e)
    {
        if (e == Event.enemyDied)
        {
            numEnemiesRemaining--;

            if(numEnemiesRemaining == 0)
            {
                WaveOver();
            }
        }

        if (e == Event.waveEnded)
        {
            StartCoroutine(StartWave());
        }
    }

    // Use this for initialization
    void Start () {

        Enemies = GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in Enemies)
        {
            enemy.Subscribe(this);
            enemy.gameObject.SetActive(false);
        }

        //if there is a previous wave, subscribe so we know when it ends
        if (previousWave != null)
        {
            previousWave.Subscribe(this);
        }
        else //otherwise this must be the first wave, start it now
        {
            StartCoroutine(StartWave());
        }
	}

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(startDelay);

        numEnemiesInWave = Enemies.Length;

        foreach(Enemy enemy in Enemies)
        {
            enemy.gameObject.SetActive(true);
        }

        numEnemiesRemaining = numEnemiesInWave;
    }

    void WaveOver()
    {
        Debug.Log("WAVE IS OVER");
        foreach(Observer o in observers)
        {
            o.Notify(this, Event.waveEnded);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Subscribe(Observer o)
    {
        observers.Add(o);   
    }
}
