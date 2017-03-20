using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    GameObject target;

    Rigidbody rigidbody;

    Vector3 movementVector;

    float zMovement;

    [SerializeField]
    Rigidbody projectile;

    [SerializeField]
    float bulletspeed = 10;

    [SerializeField]
    GameObject firingPoint;

    [SerializeField]
    float bulletSpread;

    [SerializeField]
    GameObject gun;

    [SerializeField]
    string horizontal;

    [SerializeField]
    string vertical;

    [SerializeField]
    string fire;

    [SerializeField]
    string altfire;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        bulletSpread = 0;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        BasicMovementInput();
        BodyRotation();
        GunRotation();
        ShootingControls();



    }

    private void GunRotation()
    {
        Vector3 targetDir = target.transform.position - gun.transform.position;
        float turnSpeed = movementSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed, 0.0F);
        gun.transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void ShootingControls()
    {
        Vector3 bulletSpreadPattern = new Vector3((transform.rotation.x + (Random.Range(-1f, 1f) * bulletSpread)),
                    (transform.rotation.y + (Random.Range(-1f, 1f) * bulletSpread)), 0);

        if (Input.GetAxis(fire) > 0)
        {

            Rigidbody clone = (Rigidbody)Instantiate(projectile, firingPoint.transform.position, firingPoint.transform.rotation);
            clone.name = gameObject.name;
            clone.transform.Rotate(bulletSpreadPattern);
            clone.AddForce(clone.transform.forward * bulletspeed);
            
            if (bulletSpread < 20)
            {
                bulletSpread = bulletSpread + .05f;
            }
        }
    

        else
        {
            if (bulletSpread > 0)
            {
                bulletSpread = bulletSpread - .1f;
            }
        }
    }

    private void BodyRotation()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float turnSpeed = movementSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void BasicMovementInput()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);

        //if (dist < 2)
        {
            zMovement = Input.GetAxis(vertical) * movementSpeed * Time.deltaTime;
        }

        float xMovement = Input.GetAxis(horizontal) * movementSpeed * Time.deltaTime;

        Vector3 move = new Vector3(xMovement, 0, zMovement);        

        transform.Translate(move);
        

        //move = rigidbody.transform.InverseTransformDirection(move);

        //Vector3 move = rigidbody.transform.InverseTransformDirection(xMovement, 0, zMovement);

        //rigidbody.MovePosition(rigidbody.transform.localPosition + move); 

        
    }
}
