using UnityEngine;
using System.Collections;
using System;

public class HazardZone : MonoBehaviour {

    //Handles the player falling off the level 

    [SerializeField]
    private LayerMask playerMask;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] playerColliders = Physics2D.OverlapAreaAll(new Vector2(0, -125), new Vector2(400, -175), playerMask); //finds all the colliders in an area below the map

        for (int i = 0; i < playerColliders.Length; i++) // goes through those colliders
        {
            Rigidbody2D targetrigidbody = playerColliders[i].GetComponent<Rigidbody2D>(); //finds their rigidbody

            if (!targetrigidbody)
                continue;
            
            PlayerHealth playerHealth = targetrigidbody.GetComponent<PlayerHealth>(); //find their health component
            
            if (!playerHealth)
                continue;

            playerHealth.StartCoroutine("CueDeath"); //jumps to PlayerHealth
        }
        
    }
}
