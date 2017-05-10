using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider DeathTrigger)
    {
        if (DeathTrigger.gameObject.name == "DeathTrigger")
        {
            SceneManager.LoadScene("Title");
        }
    }
}
