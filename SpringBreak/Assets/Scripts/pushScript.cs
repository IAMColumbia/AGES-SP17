using UnityEngine;
using System.Collections;

public class pushScript : MonoBehaviour {

    // Use this for initialization
    void OnTriggerEnter(Collision other)
    {
        Rigidbody playerRigidBody;
        playerRigidBody = other.gameObject.GetComponent<Rigidbody>();
       
        if (other.gameObject.tag == "Player")
        {          
                float force = 2500;
                // Calculate Angle Between the collision point and the player
                Vector3 dir = other.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
         
            playerRigidBody.AddForce(Vector3.up * 45);
            playerRigidBody.AddForce(dir * force);           
        }
    }
}
