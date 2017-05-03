using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    Sprite pingImage;

    SonarScreen sonarScreen;
    SonarPing sonarPing;

    Transform playerLocation;

    float theta, range;

    float minRange = 15;
    float maxRange = 85;

    [SerializeField]
    float moveSpeed = 2, attackRate = 7;

    float timeOfLastAttack = 0;

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

    void FixedUpdate()
    {
        if (range > minRange)
        {
            range -= moveSpeed * Time.fixedDeltaTime;

            range = Mathf.Clamp(range, minRange, maxRange);

            transform.localPosition = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) * range;

            sonarPing.MoveTo(sonarScreen.GetAnchoredPosition(theta, range));
        }
        else if(range <= minRange && Time.time > timeOfLastAttack + attackRate)
        {
            timeOfLastAttack = Time.time;
            FindObjectOfType<Player>().EnemyAttack(Random.Range(7, 10), 3);
        }
    }

    public void SpawnAtRandomLocation()
    {
        theta = Random.Range(0, 6.28f);
        range = maxRange;

        transform.localPosition = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) * range;

        ActivatePing();

        if (pingImage != null)
        {
            sonarPing.SetImage(pingImage);
        }
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
