using UnityEngine;
using System.Collections;
using System;

public class BGManager : MonoBehaviour, Observer {

    int bgIndex = 0;

    [SerializeField]
    GameObject[] BGs;

    public void Notify(Subject sender, Event e)
    {
        if(e == Event.waveEnded)
        {
            bgIndex++;
            if(bgIndex >= BGs.Length)
            {
                bgIndex = 0;
            }

            ShowBG(bgIndex);
        }
    }

    // Use this for initialization
    void Start () {
        Wave[] waves = FindObjectsOfType<Wave>();
        foreach(Wave wave in waves)
        {
            wave.Subscribe(this);
        }

        ShowBG(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ShowBG(int index)
    {
        foreach(GameObject bg in BGs)
        {
            bg.SetActive(false);
        }

        BGs[index].SetActive(true);
    }
}
