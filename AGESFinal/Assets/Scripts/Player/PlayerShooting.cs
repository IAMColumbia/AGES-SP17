using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private float bulletLaunchForce = 50;
    
    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private Rigidbody2D bulletPrefab;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject GunAxis;
    
    private float ShootingInputAxis;
    private float horizontalShootingAxis;
    private float verticalShootingAxis;
    private float fireRate = .25f;
    private float nextFire = 0;
    
    private int score;

    private GameManager gmanager;
    private PlayerController playercontroller;
    private PlayerManager player;

    private void Start()
    {
        gmanager = FindObjectOfType<GameManager>();
        audioSource.GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        GetAxis();
        HandleAimingDirection();
        Fire();

    }
    
    private void Fire()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) )
        {
            audioSource.Play();

            Rigidbody2D spawnBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as Rigidbody2D;

            spawnBullet.velocity = bulletLaunchForce * bulletSpawnPoint.transform.up;

            spawnBullet.GetComponent<BulletScript>().shooter = transform;
            
        }
    }

    private void AddToScore(int points)
    {
        gmanager.Players[playerNumber].ButtsBlasted++;
        gmanager.UpdateUIButtsBlasted();
    }

    private void GetAxis()
    {
        ShootingInputAxis = Input.GetAxis("Fire" + playerNumber);
    }

    private void HandleAimingDirection()
    {

        horizontalShootingAxis = Input.GetAxis("ShootHorizontal" + playerNumber) * Time.deltaTime * 15;
        verticalShootingAxis = Input.GetAxis("ShootVertical" + playerNumber) * Time.deltaTime * 15;

        float angle = Mathf.Atan2(horizontalShootingAxis, verticalShootingAxis) * Mathf.Rad2Deg;
        
        GunAxis.transform.eulerAngles = new Vector3(0,0,angle);
    }
}
