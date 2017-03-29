using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        MeshRenderer meshRender = this.gameObject.GetComponent<MeshRenderer>();

        if (other.tag == "Player")
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            meshRender.enabled = false;
            Player1.GetComponent<TankMovement>().m_Speed = Player1.GetComponent<TankMovement>().m_Speed + 0.1f;
        }
        if (other.tag == "Player2")
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            meshRender.enabled = false;
            Player2.GetComponent<TankMovement>().m_Speed = Player2.GetComponent<TankMovement>().m_Speed + 0.1f;
        }

    }
}
