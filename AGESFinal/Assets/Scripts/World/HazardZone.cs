using UnityEngine;
using System.Collections;

public class HazardZone : MonoBehaviour {

    [SerializeField]
    private LayerMask playerMask;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] playerColliders = Physics2D.OverlapAreaAll(new Vector2(0, -125), new Vector2(400, -175), playerMask);

        for (int i = 0; i < playerColliders.Length; i++)
        {
            Rigidbody2D targetrigidbody = playerColliders[i].GetComponent<Rigidbody2D>();

            if (!targetrigidbody)
                continue;
            
            PlayerHealth playerHealth = targetrigidbody.GetComponent<PlayerHealth>();
            
            if (!playerHealth)
                continue;

            playerHealth.CueHazardDeathParticles();
        }
        
    }
    
}
