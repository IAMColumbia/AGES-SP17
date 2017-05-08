using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DeathandRespawn : MonoBehaviour {

    [SerializeField]
    Transform respawnPoint;

    [SerializeField]
    GameObject deathScreenPanel;

    [SerializeField]
    MonoBehaviour firstPersonController;


    public bool isPlayerDead;

	// Use this for initialization
	void Start ()
    {
        firstPersonController = GetComponent<MonoBehaviour>();
        deathScreenPanel = GameObject.Find ("DeathPanel");

        firstPersonController.enabled = true;
        deathScreenPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Respawn();
       // Restart();
	}


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "DeathGround")
        {
            isPlayerDead = true;
            firstPersonController.enabled = false;

            deathScreenPanel.gameObject.SetActive(true);
            gameObject.transform.position = respawnPoint.position;
        }
    }

    void Respawn()
    {
        if (isPlayerDead == true)
        {
            if (Input.GetButtonDown("Respawn"))
            {
                deathScreenPanel.gameObject.SetActive(false);
                firstPersonController.enabled = true;
            }
        }
    }
   /* private void Restart()
    {
       if(Input.GetButtonDown("Respawn"))
        {
            gameObject.transform.position = respawnPoint.position;
        }
    } */
}
