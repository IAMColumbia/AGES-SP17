using UnityEngine;
using System.Collections;
using System;

//I accidentally mistyped this scripts name, but I think it's funny, so I'm going to keep it
public class aManagerGun : MonoBehaviour
{
    [SerializeField]
    bool playerCanShoot;

    [SerializeField]
    RaycastShoot raycastShoot;

    [SerializeField]
    GameObject shooterFPSWeapon;

    // Use this for initialization
    void Start ()
    {
        raycastShoot = GetComponent<RaycastShoot>();
        shooterFPSWeapon = GetComponentInChildren<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GunAvailability();
        Debug.Log("Can player shoot?  " + playerCanShoot.ToString());
	}

    private void GunAvailability()
    {
        if (playerCanShoot == true)
        {
            raycastShoot.enabled = true;
            shooterFPSWeapon.SetActive(true);
        }
        else if (playerCanShoot == false)
        {
            raycastShoot.enabled = false;
            shooterFPSWeapon.SetActive(false);
        }
    }
}
