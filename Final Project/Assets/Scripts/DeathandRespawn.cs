using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        deathScreenPanel = GameObject.Find ("Panel");

        firstPersonController.enabled = true;
        deathScreenPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Respawn();
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "DeathGround")
        {
            firstPersonController.enabled = false;

            deathScreenPanel.gameObject.SetActive(true);
            isPlayerDead = true;
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
                isPlayerDead = false;
            }
        }
    }
}
