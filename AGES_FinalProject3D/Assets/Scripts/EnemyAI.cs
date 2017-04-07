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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    private void Explode()
    {
    }
}
