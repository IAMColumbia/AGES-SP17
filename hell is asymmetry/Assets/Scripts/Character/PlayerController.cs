using UnityEngine;
using System.Collections;


public class Character : MonoBehaviour
{
    public float Score { get; protected set; }
    public void AddScore(float points)
    {
        Score += points;
    }
}

public interface IDamageable
{
    void takeDamage(Bullet bullet);
}

public class PlayerController : Character, IDamageable {

    [SerializeField]
    string m_playerLetter;

    [SerializeField]
    float maxHealth;

    public float Health { get; private set; }

    string verticalAxisName;
    string horizontalAxisName;
    string fireButtonName;

    float horizontalInput;
    float verticalInput;
    bool fireInput;

    bool Alive;

    float maxX;
    float maxY;

    [SerializeField]
    float horizontalMoveSpeed, verticalMoveSpeed, fireRate;

    [SerializeField]
    Transform[] firingPositions;

    [SerializeField]
    Bullet bullet;

    float timeBetweenShots;
    bool shooting = false;
    bool readyToShoot = true;

	// Use this for initialization
	void Start () {
        Health = maxHealth;
        Score = 0;
        verticalAxisName = "Vertical" + m_playerLetter;
        horizontalAxisName = "Horizontal" + m_playerLetter;
        fireButtonName = "Fire" + m_playerLetter;

        BoxCollider2D boundary = GameObject.FindWithTag("Boundary").GetComponent<BoxCollider2D>();
        maxX = boundary.size.x / 2;
        maxY = boundary.size.y / 2;

        timeBetweenShots = 1 / fireRate;
}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        UpdateMovement(Time.deltaTime);
        UpdateShooting();
	}

    void GetInput()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);
        verticalInput = Input.GetAxis(verticalAxisName);
        fireInput = Input.GetButton(fireButtonName);
    }

    void UpdateShooting()
    {
        //probably going to revisit this, prevents player from shooting faster than the specified fire rate
        if(fireInput && !shooting && readyToShoot)
        {
            shooting = true;
            StartCoroutine(KeepShooting());
        }
        if(!fireInput && shooting)
        {
            shooting = false;
        }
    }

    IEnumerator KeepShooting()
    {
        while (shooting)
        {
            Shoot(firingPositions[0]);
            readyToShoot = false;
            yield return new WaitForSeconds(timeBetweenShots);
            readyToShoot = true;
        }
    }

    void UpdateMovement(float deltaTime)
    {
        Vector3 currentPos = transform.position;

        currentPos.y += verticalInput * verticalMoveSpeed * deltaTime;
        currentPos.x += horizontalInput * horizontalMoveSpeed * deltaTime;

        currentPos.x = Mathf.Clamp(currentPos.x, -maxX, maxX);
        currentPos.y = Mathf.Clamp(currentPos.y, -maxY, maxY);

        transform.position = currentPos;
    }

    public void takeDamage(Bullet bullet)
    {
        Health -= bullet.damage;
        
        if(Health <= 0 && Alive)
        {
            Die();
        }
    }

    public void Die()
    {
        Alive = false;
        
    }

    public void Shoot(Transform firingPosition)
    {
        float tempBulletSpeed = 10;

        Bullet newBullet = Instantiate<Bullet>(bullet);
        newBullet.transform.position = firingPosition.position;
        newBullet.Init(true, this.gameObject.layer, firingPosition.forward * tempBulletSpeed, this);
    }
}
