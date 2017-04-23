using UnityEngine;
using System.Collections;

public class WaterBlast : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject waterBlast;
    [SerializeField]
    GameObject waterBlastEmitter;
    [SerializeField]
    float bulletForwardForce;
    [SerializeField]
    GameObject pushCollider;

    public void waterBlastAttack () {

        Debug.Log("Enemy blast now!");
        GameObject temporaryBulletHandler;
        temporaryBulletHandler = Instantiate(waterBlast, waterBlastEmitter.transform.position, waterBlastEmitter.transform.rotation) as GameObject;
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = temporaryBulletHandler.GetComponent<Rigidbody>();
        
        // If the object we hit is the enemy        
        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        //Temporary_RigidBody.AddForce(transform.forward * bulletForwardForce * Time.deltaTime);
        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        Destroy(temporaryBulletHandler, 2f);
    }
	
	// Update is called once per frame
	                   
  
 }
