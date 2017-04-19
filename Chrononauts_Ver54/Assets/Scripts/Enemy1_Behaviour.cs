using UnityEngine;
using System.Collections;

public class Enemy1_Behaviour : MonoBehaviour {

    [SerializeField]
    public Bullet bulletPrefab;

    [SerializeField]
    GameObject bulletSpawner1;

    [SerializeField]
    public float bulletFireCooldown = 0.01f;
    
    private bool canFire = true;

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        Fire();
        EnemyCleanUp();
    }

    //Perkele I am depress I am kill self
    void EnemyCleanUp()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            Debug.Log("Disabling object");
            this.gameObject.SetActive(false);
        }
    }

    void CreateBullets()
    {
        //I know these error, but it works, so just try to avoid touching the i
        Bullet bullet;
        bullet = Instantiate(bulletPrefab, bulletSpawner1.transform.position, bulletSpawner1.transform.rotation) as Bullet;
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
