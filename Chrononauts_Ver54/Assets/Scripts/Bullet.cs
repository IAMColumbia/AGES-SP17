using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    int bulletSpeed = 5;
    [SerializeField]
    string enemyTag;

    Rigidbody2D bulletHull;

    private float timeToDestroyBullet = 10f;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, timeToDestroyBullet);
        bulletHull = this.gameObject.GetComponent<Rigidbody2D>();
        bulletHull.AddForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == enemyTag)
        {
            Debug.Log("Encountered enemy!");
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().playerControlActive = false;
            }
            this.gameObject.SetActive(false);
        }
    }

}
