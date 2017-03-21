using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Enemy : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject player;

    [SerializeField]
    float playerDistance;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float rotationDamping;

    //[SerializeField]
    //GameObject waterPlane;

    float isMoving; 

    public int sceneToStart = 2;


    

    //Animator anim;

    //Animation animation;

    //Rigidbody rigidBody; 

   
    bool isDead = false;



    void Start()
    {


        //anim = GetComponent<Animator>();
        //animation = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
       // if (player == null)
            player = GameObject.FindWithTag("Player");
        
           playerDistance = Vector3.Distance(player.transform.position, transform.position);              
           lookAtPlayer();            
           enemyMotion();
        
        if (isDead == true)
        {
          movementSpeed = 0;      
        }       

	}

    private void fireWeapon()
    {
        
    }

    private void lookAwayFromPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.transform.position + transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    

    private void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.SetActive(false);
            SceneManager.LoadScene(sceneToStart);
        }
        if (other.gameObject.tag == "Projectile")
        {
            isDead = true;
            //anim.SetBool("isDead", true);
           // anim.Play("PA_WarriorDeath_Clip");

        }
    }     
   
    private void enemyMotion()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
     
    }
    
}
