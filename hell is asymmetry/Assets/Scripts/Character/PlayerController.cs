using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour
{
    public float Score { get; protected set; }
    public void AddScore(float points)
    {
        Score += points;

        if (Score < 0)
        {
            Score = 0;
        }
    }

    public virtual void hitSuccess()
    {

    }

    public virtual void killSuccess()
    {

    }
}

public interface IDamageable
{
    void takeDamage(Bullet bullet);
}

public class PlayerController : Character, IDamageable, Subject {

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
    bool alive;

    float respawnCooldownTime = 3f;
    bool readyToSpawn = false;

    public bool Alive
    {
        get
        {
            return alive;
        }
        private set
        {
            alive = value;

            m_collider.enabled = value;
            foreach(MeshRenderer r in renderers)
            {
                r.enabled = value;
            }
        }
    }

    float maxX;
    float maxY;

    [SerializeField]
    MeshRenderer[] renderers;

    [SerializeField]
    float horizontalMoveSpeed, verticalMoveSpeed, fireRate;

    [SerializeField]
    Transform[] firingPositions;

    [SerializeField]
    Bullet bullet;

    float timeBetweenShots;
    float timeOfLastShot = 0;

    PlayerController otherPlayer;

    Collider2D m_collider;

    spawnTimer m_timer;

    List<Observer> observers = new List<Observer>();

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

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach(PlayerController player in players)
        {
            if(player != this)
            {
                otherPlayer = player;
            }
        }

        m_collider = GetComponent<Collider2D>();

        m_timer = GetComponentInChildren<spawnTimer>();

        Alive = true;
}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        UpdateMovement(Time.deltaTime);
        UpdateShooting();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            HalfEnemy damageable = collision.collider.gameObject.GetComponent<HalfEnemy>();

            damageable.takeDamage(5);

            this.takeDamage(9999);
        }
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);
        verticalInput = Input.GetAxis(verticalAxisName);
        fireInput = Input.GetButton(fireButtonName);
    }

    void UpdateShooting()
    {
        if (Alive)
        {
            if (fireInput && Time.time > timeOfLastShot + timeBetweenShots)
            {
                timeOfLastShot = Time.time;
                Shoot(firingPositions[0]);
            }
        }
        else
        {
            if (fireInput && readyToSpawn)
            {
                readyToSpawn = false;
                Respawn();
            }
        }
    }

    void UpdateMovement(float deltaTime)
    {
        if (Alive)
        {
            Vector3 currentPos = transform.position;

            currentPos.y += verticalInput * verticalMoveSpeed * deltaTime;
            currentPos.x += horizontalInput * horizontalMoveSpeed * deltaTime;

            currentPos.x = Mathf.Clamp(currentPos.x, -maxX, maxX);
            currentPos.y = Mathf.Clamp(currentPos.y, -maxY, maxY);

            transform.position = currentPos;
        }
        else
        {
            transform.position = otherPlayer.transform.position;
        }
    }

    public void takeDamage(Bullet bullet)
    {
        Health -= bullet.damage;
        
        if(Health <= 0 && Alive)
        {
            Die();
        }
    }

    public void takeDamage(float amount)
    {
        Health -= amount;

        if (Health <= 0 && Alive)
        {
            Die();
        }
    }

    public void RespawnTimerFinished()
    {
        readyToSpawn = true;
    }

    public void Die()
    {
        StopAllCoroutines();

        spawnTimer.spawnTimerCallback timerCallback = RespawnTimerFinished;

        m_timer.StartTimer(respawnCooldownTime, timerCallback);

        Alive = false;
        AddScore(-500);

        foreach (Observer o in observers)
        {
            o.Notify(this, Event.playerDied);
        }
    }

    public void Respawn()
    {
        m_timer.HideTimer();
        Health = maxHealth;
        Alive = true;
    }

    public void Shoot(Transform firingPosition)
    {
        float tempBulletSpeed = 10;

        Bullet newBullet = Instantiate<Bullet>(bullet);
        newBullet.transform.position = firingPosition.position;
        newBullet.Init(true, this.gameObject.layer, firingPosition.forward * tempBulletSpeed, this);

        foreach (Observer o in observers)
        {
            o.Notify(this, Event.firedBullet);
        }
    }

    public override void hitSuccess()
    {
        foreach(Observer o in observers)
        {
            o.Notify(this, Event.hitEnemy);
        }
    }

    public override void killSuccess()
    {
        foreach (Observer o in observers)
        {
            o.Notify(this, Event.killedEnemy);
        }
    }

    public void Subscribe(Observer o)
    {
        observers.Add(o);
    }
}
