using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    SonarScreen sonarScreen;
    SonarPing sonarPing;

    Transform playerLocation;

    float theta, range;

    float minRange = 15;
    float maxRange = 85;

	// Use this for initialization
	void Start () {
        sonarScreen = FindObjectOfType<SonarScreen>();
        playerLocation = FindObjectOfType<TorpedoLauncher>().transform;

        transform.SetParent(playerLocation, false);

        SpawnAtRandomLocation();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnAtRandomLocation()
    {
        theta = Random.Range(0, 6.28f);
        range = Random.Range(minRange, maxRange);

        transform.localPosition = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) * range;

        ActivatePing();
    }

    public void ActivatePing()
    {
        sonarPing = sonarScreen.SpawnPingAt(theta, range);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Torpedo")
        {
            Torpedo t = other.gameObject.GetComponent<Torpedo>();

            if(t != null)
            {
                t.HitEnemy();
                sonarScreen.RemovePing(sonarPing);
                SpawnAtRandomLocation();
            }
        }
    }
}
