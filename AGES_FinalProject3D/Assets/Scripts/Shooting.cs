using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private string shootButton;
    [SerializeField]
    private Rigidbody bullet;
    [SerializeField]
    private float bulletSpeed;

    private const float zeroConstant = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Shoot();
	}

    void Shoot()
    {
        if (Input.GetButtonDown(shootButton))
        {
            Debug.Log("Shot fired!");
            Rigidbody bulletInstance = Instantiate(bullet) as Rigidbody;
            bulletInstance.AddForce(zeroConstant, zeroConstant, bulletSpeed);
        }
    }
}
