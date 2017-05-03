using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {


    [SerializeField]
    public Bullet bulletPrefab;

    [SerializeField]
    GameObject bulletSpawner1;
    [SerializeField]
    GameObject bulletSpawner2;
    [SerializeField]
    GameObject bulletSpawner3;

    [SerializeField]
    public int bulletType = 1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateBullets()
    {
        if (bulletType == 1)
        {
            //I know these error, but it works, so just try to avoid touching it, if you please.
            Bullet bullet;
            bullet = Instantiate(bulletPrefab, bulletSpawner2.transform.position, bulletSpawner2.transform.rotation) as Bullet;
        }
        if (bulletType == 2)
        {
            //I know these error, but it works, so just try to avoid touching it, if you please.
            Bullet bullet;
            bullet = Instantiate(bulletPrefab, bulletSpawner1.transform.position, bulletSpawner1.transform.rotation) as Bullet;
            Bullet bullet2;
            bullet2 = Instantiate(bulletPrefab, bulletSpawner2.transform.position, bulletSpawner2.transform.rotation) as Bullet;
            Bullet bullet3;
            bullet3 = Instantiate(bulletPrefab, bulletSpawner3.transform.position, bulletSpawner3.transform.rotation) as Bullet;
        }
    }
}
