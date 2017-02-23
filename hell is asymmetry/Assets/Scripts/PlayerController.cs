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

    string verticalAxisName;
    string horizontalAxisName;
    string fireButtonName;

    float horizontalInput;
    float verticalInput;
    bool fireInput;

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

	// Use this for initialization
	void Start () {
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
        if(fireInput && !shooting)
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
            yield return new WaitForSeconds(timeBetweenShots);
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
        Debug.Log(gameObject.name + " was hit by " + bullet.owner.name);
    }

    public void Shoot(Transform firingPosition)
    {
        Bullet newBullet = Instantiate<Bullet>(bullet);
        newBullet.transform.position = firingPosition.position;
        newBullet.Init(true, this.gameObject.layer, firingPosition.forward * 2, this);
    }
}
