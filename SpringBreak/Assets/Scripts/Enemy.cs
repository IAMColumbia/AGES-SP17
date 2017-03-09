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
        //playerMotion();
        

        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        //if(transform.position.y == waterPlane.transform.position.y)
        //{
        // //   transform.position.y = waterPlane.transform.position.y - 1; 
        //}
        if (isDead == false && playerDistance > 30f)
        {
            //anim.Play("PA_WarriorIdle_Clip");
            lookAtPlayer();
        }
        if (isDead == false && playerDistance < 25f)
        {
           lookAtPlayer();
           enemyMotion();
            fireWeapon();
         
        }
        if (isDead == true)
        {
          movementSpeed = 0; 
          //anim.Play("PA_WarriorDeath_Clip");
            //animation.Play("PA_WarriorDeath_Clip");
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
        //animation.Play("PA_WarriorForward_Clip");
        // if (isMoving == movementSpeed)
        // {
        //     anim.SetFloat("isMoving", 1);
        // }
        //else 
        //         {
        //     anim.SetFloat("isMoving", 0);
        // }


    }
    
}
