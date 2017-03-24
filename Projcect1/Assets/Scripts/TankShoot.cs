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
    private GameObject bullet;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void FixedUpdate()
    {
        ShootingHandler();
    }

    private void ShootingHandler()
    {
        if (Input.GetAxis(shootForwardButton) >= .05)
        {
            ShootFromFront();
        }
        if (Input.GetAxis(shootBackwardButton) >= .05)
        {
            ShootFromBack();
        }
    }

    private void ShootFromFront()
    {
        Debug.Log("Shot from front has been fired!");
    }

    private void ShootFromBack()
    {
        Debug.Log("Shot from back has been fired!");
    }
}
