using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private string playerNumber;

    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private Rigidbody2D bulletPrefab;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float bulletLaunchForce = 50;

    private string ShootingInputAxis;
    private float horizontalShootingAxis;
    private float verticalShootingAxis;

    private GameManager gmanager;
    private PlayerController player;

    private void Start()
    {
        gmanager = FindObjectOfType<GameManager>();
        player = GameObject.Find("Player" + playerNumber).GetComponent<PlayerController>();

    }
    private void Update()
    {
        GetAxis();
        HandleAimingDirection();

        if(Input.GetButtonDown(ShootingInputAxis))
        {

            Rigidbody2D spawnBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as Rigidbody2D;

            spawnBullet.velocity = bulletLaunchForce * bulletSpawnPoint.transform.up;
            
        }
    }

    private void GetAxis()
    {
        ShootingInputAxis = "Fire" + playerNumber;
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
