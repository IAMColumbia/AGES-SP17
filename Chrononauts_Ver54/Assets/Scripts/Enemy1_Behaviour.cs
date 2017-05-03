using UnityEngine;
using System.Collections;

public class Enemy1_Behaviour : MonoBehaviour {

    //TODO: Every enemy is going to have a specific animation!
    //      Make it so that there's a reference for what the enemy's animation is.
    //TODO: Renderer.isVisible for optimization.e

    [SerializeField]
    public Bullet bulletPrefab;

    [SerializeField]
    GameObject bulletSpawner1;
    [SerializeField]
    GameObject bulletSpawner2;
    [SerializeField]
    GameObject bulletSpawner3;

    [SerializeField]
    public float bulletFireCooldown = 0.01f;

    [SerializeField]
    public int bulletType = 1;

    [SerializeField]
    public TimeController tc;
    
    public bool canFire = true;
    public bool animationIsPlaying = false;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        EnemyCleanUp();
    }

    //Perkele I am depress I am kill self
    void EnemyCleanUp()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            tc.gameObject.GetComponent<TimeController>().globalTimer = tc.gameObject.GetComponent<TimeController>().globalTimer + 2;
            Debug.Log("Disabling object");
            this.gameObject.SetActive(false);
        }
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
        }


    }

    private void Fire()
    {
        if (canFire)
        {
            CreateBullets();
            canFire = false;
            StartCoroutine(HandleCooldown());
        }
    }

    private IEnumerator HandleCooldown()
    {
        yield return new WaitForSeconds(bulletFireCooldown);

        canFire = true;
    }
}
