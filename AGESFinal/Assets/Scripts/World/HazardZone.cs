using UnityEngine;
using System.Collections;
using System;

public class HazardZone : MonoBehaviour {

    //Handles the player falling off the level 

    [SerializeField]
    private LayerMask playerMask;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] playerColliders = Physics2D.OverlapAreaAll(new Vector2(0, -125), new Vector2(400, -175), playerMask); 
        
        for (int i = 0; i < playerColliders.Length; i++) 
        {
            BoxCollider2D targetButt = playerColliders[i].GetComponent<BoxCollider2D>(); 
            
            if (!targetButt)
                continue;
            
            PlayerHealth playerHealth = targetButt.GetComponentInChildren<PlayerHealth>(); 
            
            if (!playerHealth)
                continue;

            playerHealth.CueDeath(); 
        }
        
    }
}
