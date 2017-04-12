using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static float deathTimer = -1;
    public Bullet bulletPrefab;

    public Vector2 velocity;
    public float speed;
    public float acceleration = 0.5f;
    public Vector3 keyDirection;
    public int bulletcounter = 0;

    public DifficultyManager dm;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (deathTimer > 0)
        {
            return;
        }
        if (deathTimer < Time.time)
        {

        }

        BulletControls();
        PlayerMovement();

    }

    private void BulletControls()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Bullet tempBullet;
            tempBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, -90)) as Bullet;
        }
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Bullet tempBullet;
        //    tempBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, -45)) as Bullet;
        //}
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Bullet tempBullet;
            tempBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, 0)) as Bullet;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Bullet tempBullet;
            tempBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, 90)) as Bullet;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Bullet tempBullet;
            tempBullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, 180)) as Bullet;
        }
    }

    private void PlayerMovement()
    {
        keyDirection.x = keyDirection.y = 0;

        if (Input.GetKey("right"))
        {
            keyDirection.x += 1;
        }
        if (Input.GetKey("left"))
        {
            keyDirection.x += -1;
        }
        if (Input.GetKey("up"))
        {
            keyDirection.y += 1;
        }
        if (Input.GetKey("down"))
        {
            keyDirection.y += -1;
        }

        keyDirection.Normalize();

        //velocity = Vector2.MoveTowards(velocity, this.keyDirection * this.speed, Time.deltaTime * acceleration);
        this.transform.Translate(new Vector3(keyDirection.x, keyDirection.y, 0) * (speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bug")
        {
            deathTimer = Time.time + 5;
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    void RespawnPlayer()
    {
        transform.position = new Vector3(0, 0, 0);
        this.gameObject.GetComponent<Renderer>().enabled = true;
        deathTimer = -1;
    }

}
