using UnityEngine;
using System.Collections;
using System;

public class EnemyAI : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Explode();
        }
    }

    public void Explode()
    {
        gameObject.SetActive(false);
    }
}
