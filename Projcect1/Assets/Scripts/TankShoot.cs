using UnityEngine;
using System.Collections;

public class TankShoot : MonoBehaviour
{
    [SerializeField]
    private string shootForwardButton;
    [SerializeField]
    private string shootBackwardButton;
    [SerializeField]
    private Transform frontShotSpawnLocation;
    [SerializeField]
    private Transform backShotSpawnLocation;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private float bulletLaunchForceFromFront = 100;
    [SerializeField]
    private float bulletLaunchForceFromBack = -100;

	
	// Update is called once per frame
	void Update ()
    {
        ShootingHandler();
    }

    private void ShootingHandler()
    {
        if (Input.GetButtonDown(shootForwardButton))
        {
            ShootFromFront();
        }
        if (Input.GetButtonDown(shootBackwardButton))
        {
            ShootFromBack();
        }
    }

    private void ShootFromFront()
    {
        Debug.Log("Shot from front has been fired!");
        Bullet bulletClone = (Bullet) Instantiate(bullet, frontShotSpawnLocation.position, frontShotSpawnLocation.rotation);
        bulletClone.LaunchBullet(bulletLaunchForceFromFront);
    }

    private void ShootFromBack()
    {
        Debug.Log("Shot from back has been fired!");
        Bullet bulletClone = (Bullet) Instantiate(bullet, backShotSpawnLocation.position, backShotSpawnLocation.rotation);
        bulletClone.LaunchBullet(bulletLaunchForceFromBack);
    }
}
