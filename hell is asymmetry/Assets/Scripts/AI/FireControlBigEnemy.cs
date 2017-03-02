using UnityEngine;
using System.Collections;

public class FireControlBigEnemy : MonoBehaviour
{

    Enemy enemy;

    [SerializeField]
    float rateOfFire, timeBetweenBursts, shotsPerBurst;

    float timeBetweenShots;
    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<Enemy>();
        timeBetweenShots = 1 / rateOfFire;
        StartCoroutine(KeepShooting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator KeepShooting()
    {
        while (enemy.Alive)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < shotsPerBurst; j++)
                {
                    enemy.Shoot(enemy.firingPositions[i * 2]);
                    enemy.Shoot(enemy.firingPositions[i * 2 + 1]);
                    yield return new WaitForSeconds(timeBetweenShots);
                }
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }
    }
}