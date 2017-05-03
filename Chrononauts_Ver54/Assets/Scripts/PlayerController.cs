using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //NOTES REGARDING THE BEHAVIOUR OF THE PLAYER
    //1. When a player is created, they're created below the screen, and an animation plays that places them in the spawn position. 
    //2. Player control is disabled until the animation is finished being played. 
    //DONE 3. Player control consists of 2 degrees of movement, X and Y. 
    //DONE 4. Player speed is altered depending on how they move. If the players are moving in reverse, or side to side, their speed is reduced.
    //DONE 5. If the player is moving forward, either diagonally, or straight, they are moving at "Full speed" **DONE IN THE SpeedRegulator() FUNCTION!!**
    //DONE 6. The player can shoot by pressing the fire button. This instantiates a set of bullets, specific to the player's current powerup.
    //DONE 7. The player can either shoot by tapping the fire button, or by holding down the fire button. 
    //8. The player will die if they touch a single bullet that is not their own. 
    //9. When a player dies, we check to see if they have any lives left. If they have lives left, we spawn them with a different colour, and subtract a life.

    [SerializeField]
    public float movementSpeedModifier = 5f;

    [SerializeField]
    public Bullet bulletPrefab;

    [SerializeField]
    GameObject bulletSpawner1;

    [SerializeField]
    GameObject bulletSpawner2;

    public Vector3 keyDirection;
    public Animator shipAnimator;

    [SerializeField]
    public float bulletFireCooldown = 0.5f;

    private bool isFiringOnCooldown = false;
    public bool playerControlActive = true;

    // Use this for initialization
    void Start ()
    {
        shipAnimator = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpeedRegulator();
        PlayerMovement();
        PlayerShooting();
        SpriteUpdate();
        PlayerDeath();
	}

    //This function is run when a new player is created. A short animation is played to move the player onto the screen, during which they are invincible, and 
    //cannot move. At the end of this period, players are granted movement, and become vulnerable.
    void PlayerSpawn()
    {

    }

    //This function regulates the speed variable. Basically, if the player presses back, or the side buttons, it reduces the player's speed. If the forward button is
    //pressed, it returns the speed to its regular value.
    void SpeedRegulator()
    {
        if (Input.GetKey("right"))
        {
            movementSpeedModifier = 3.5f;
        }
        if (Input.GetKey("left"))
        {
            movementSpeedModifier = 3.5f;
        }
        if (Input.GetKey("up"))
        {
            movementSpeedModifier = 5;
        }
        if (Input.GetKey("down"))
        {
            movementSpeedModifier = 2.5f;
        }
    }

    //This function recieves input and moves the player accordingly. Players will be moved in the appropriate axis, and will be multiplied by the current speed. 
    //Ideally, this should work in tandem with the Speed Regulator funciton, so that when the player is moving back, they're moving more slowly.
    void PlayerMovement()
    {
        //TODO: Use Real Input!
        keyDirection.x = keyDirection.y = 0;
        if (playerControlActive)
        {
            if (Input.GetKey("right"))
            {
                keyDirection.x += 1;
            }
            if (Input.GetKey("left"))
            {
                keyDirection.x += -1;
            }
            if (Input.GetKey("up"))
            {
                keyDirection.y += 1;
            }
            if (Input.GetKey("down"))
            {
                keyDirection.y += -1;
            }
        }

        keyDirection.Normalize();

        //velocity = Vector2.MoveTowards(velocity, this.keyDirection * this.speed, Time.deltaTime * acceleration);
        this.transform.Translate(new Vector3(keyDirection.x, keyDirection.y, 0) * (movementSpeedModifier * Time.deltaTime));
    }

    //This function dictates the player's shooting ability. First, we do a check for what player powerup is active. Second, we have two conditions for whether or not
    //the player is either tapping or holding the shooting button. (What we can do here is trigger coroutines that limit the player's shooting ability so it's not
    //being activated every single frame, but rather on a small interval. 
    void PlayerShooting()
    {
        //TODO: Use input manager! (Input.GetButton)
        if (Input.GetKey("a") && playerControlActive)
        {
            Fire();            
        }
    }

    private void Fire()
    {
        if (!isFiringOnCooldown)
        {
            isFiringOnCooldown = true;
            CreateBullets();
            StartCoroutine(HandleCooldown());
        }
    }

    private IEnumerator HandleCooldown()
    {
        yield return new WaitForSeconds(bulletFireCooldown);

        isFiringOnCooldown = false;
    }

    void CreateBullets()
    {

        Bullet bullet;
        Bullet bullet2;
        bullet = Instantiate(bulletPrefab, bulletSpawner1.transform.position, bulletSpawner1.transform.rotation) as Bullet;
        bullet2 = Instantiate(bulletPrefab, bulletSpawner2.transform.position, bulletSpawner2.transform.rotation) as Bullet;     
    }

    //Simple function here. Changes the player's sprite depending on if they're moving either right or left. 
    //Also changes the player's color, depending on which life is active.
    void SpriteUpdate()
    {
        //TODO: Use the actual input here for input.getkey
        if (Input.GetKey("right"))
        {
            shipAnimator.SetBool("isTiltingRight", true);
        }
        if (!Input.GetKey("right"))
        {
            shipAnimator.SetBool("isTiltingRight", false);
        }

        if (Input.GetKey("left"))
        {
            shipAnimator.SetBool("isTiltingLeft", true);
        }
        if (!Input.GetKey("left"))
        {
            shipAnimator.SetBool("isTiltingLeft", false);
        }
    }

    //This function details what happens when a player touches an enemy bullet. First, we destroy the player's sprite. Next, we create and play an explosion animation.
    //Finally, we subtract a player life from the player manager, and kill the player entity. 
    void PlayerDeath()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            StartCoroutine(HandleSpawning());
        }
    }

    private IEnumerator HandleSpawning()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        playerControlActive = true;
        
    }
}
