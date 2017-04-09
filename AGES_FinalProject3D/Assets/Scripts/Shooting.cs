using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private string shootButton;
    [SerializeField]
    private string rightStickVertical;
    [SerializeField]
    private string rightStickHorizontal;
    [SerializeField]
    private Transform reticle;
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
        RotateToReticle();
        Shoot();
	}

    void Shoot()
    {
        if (Input.GetButtonDown(shootButton))
        {
            Debug.Log("Shot fired!");
            Rigidbody bulletInstance = Instantiate(bullet,gameObject.transform.position,gameObject.transform.rotation) as Rigidbody;
            bulletInstance.AddForce(zeroConstant, bulletSpeed, zeroConstant);
        }
    }

    void RotateToReticle()
    {
        gameObject.transform.LookAt(reticle);
    }
}
