using UnityEngine;
using System.Collections;

public class FireControl : MonoBehaviour {

    Enemy enemy;

    [SerializeField]
    float rateOfFire;

    float timeBetweenShots;
	// Use this for initialization
	void Start () {
        enemy = GetComponent<Enemy>();
        timeBetweenShots = 1 / rateOfFire;
        StartCoroutine(KeepShooting());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator KeepShooting()
    {
        while (enemy.Alive)
        {

            enemy.ShootAll();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
