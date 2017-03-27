using UnityEngine;
using System.Collections;
using System;

public class TankStatus : MonoBehaviour
{

    public float TankSize = 1;
    private float increaseSize;
    private float decreaseSize;
    private Transform tankTransform;
    private AudioSource audioSource;
    GameManager gm;

	void Start ()
    {
        tankTransform = this.transform;
        increaseSize = .25f;
        decreaseSize = .25f;
        tankTransform.localScale = new Vector3(TankSize, TankSize, TankSize);
        audioSource = this.GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Consume();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            OnDeath();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tank")
        {
            CheckFoodChain(collision);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Shell")
        {
            TakeDamage(collision);
        }
    }

    private void TakeDamage(Collider c)
    {
        TankSize -= decreaseSize;
        tankTransform.localScale = new Vector3(TankSize, TankSize, TankSize);
        Destroy(c.gameObject);
    }

    private void CheckFoodChain(Collision collision)
    {
        if (collision.gameObject.GetComponent<TankStatus>().TankSize > this.TankSize)
        {
            OnDeath();
        }

        else if (collision.gameObject.GetComponent<TankStatus>().TankSize < this.TankSize)
        {
            Consume();
        }

        else
        {
            //If tank sizes are the same, perhaps play audio? Who knows.
        }
    }

    private void Consume()
    {
        TankSize += increaseSize;
        tankTransform.localScale = new Vector3(TankSize, TankSize, TankSize);
        audioSource.Play();
    }

    void OnDeath()
    {
        if (GetComponent<Shoot>() != null)
        {
            gm.numPlayers--;
        }
        Destroy(this.gameObject);
    }

}
