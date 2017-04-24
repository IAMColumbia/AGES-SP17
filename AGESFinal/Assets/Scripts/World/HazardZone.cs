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
        Debug.Log("1");
        for (int i = 0; i < playerColliders.Length; i++) 
        {
            BoxCollider2D targetButt = playerColliders[i].GetComponent<BoxCollider2D>(); 
            Debug.Log("2");
            if (!targetButt)
                continue;
            
            PlayerHealth playerHealth = targetButt.GetComponentInChildren<PlayerHealth>(); 
            Debug.Log("3");
            if (!playerHealth)
                continue;
            Debug.Log("4");
            playerHealth.CueDeath(); 
        }
        
    }
}
