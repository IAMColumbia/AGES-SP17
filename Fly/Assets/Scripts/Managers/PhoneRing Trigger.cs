using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PhoneRingTrigger : MonoBehaviour {

    // Use this for initialization
    AudioSource audioSource;

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            audioSource.Play();
        }
    }
}

