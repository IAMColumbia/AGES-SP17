﻿using UnityEngine;
using System.Collections;
using System;

public class EndOfWaveMessage : MonoBehaviour, Observer {

    [SerializeField]
    float messageTime = 3;

    [SerializeField]
    GameObject message;

    AudioSource sound;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();
        message.SetActive(false);
        Wave[] waves = FindObjectsOfType<Wave>();
        foreach (Wave wave in waves)
        {
            wave.Subscribe(this);
        }

    }

    public void Notify(Subject sender, Event e)
    {
        if(e == Event.waveEnded)
        {
            StartCoroutine(showMessage(messageTime));
        }
    }

    IEnumerator showMessage(float t)
    {
        yield return new WaitForSeconds(1);
        sound.Play();
        message.SetActive(true);
        yield return new WaitForSeconds(t);
        message.SetActive(false);
    }
}
