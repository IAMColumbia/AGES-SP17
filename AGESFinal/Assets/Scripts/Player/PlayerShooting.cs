using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private string playerNumber;

    [SerializeField]
    private float bulletLaunchForce = 50;

    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private Rigidbody2D bulletPrefab;

    [SerializeField]
    private AudioSource audioSource;

    private float ShootingInputAxis;
    private float horizontalShootingAxis;
    private float verticalShootingAxis;
    private float fireRate = .25f;
    private float nextFire = 0;

    private GameManager gmanager;
    private PlayerController player;

    private void Start()
    {
        gmanager = FindObjectOfType<GameManager>();

    }
    private void Update()
    {
        GetAxis();
        HandleAimingDirection();
        Fire();

    }
    

    private void Fire()
    {
        if (ShootingInputAxis == 1 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Rigidbody2D spawnBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as Rigidbody2D;

            spawnBullet.velocity = bulletLaunchForce * bulletSpawnPoint.transform.right;
        }
    }

    private void GetAxis()
    {
        ShootingInputAxis = Input.GetAxis("Fire" + playerNumber);
    }

    private void HandleAimingDirection()
    {
        //if(Input.GetKey(KeyCode.Space))
        //    gmanager.StoreButtsBlasted();


        horizontalShootingAxis = Input.GetAxis("ShootHorizontal" + playerNumber) * Time.deltaTime * 15;
        verticalShootingAxis = Input.GetAxis("ShootVertical" + playerNumber) * Time.deltaTime * 15;

        float angle = Mathf.Atan2(horizontalShootingAxis, verticalShootingAxis) * Mathf.Rad2Deg;

        //transform.localPosition = new Vector2(horizontalShootingAxis * 15, -1 * verticalShootingAxis * 15);

        transform.localEulerAngles = new Vector3(0, 0, angle);



        //Vector3 moveAngle = new Vector3(0, 0, 45);


        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.Rotate(transform.parent.position, 15f);
    }
}
