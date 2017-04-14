using UnityEngine;
using System.Collections;

public class HazardZone : MonoBehaviour {

    private PlayerHealth playerHealth;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.CueHazardDeathParticles();
    }
}
