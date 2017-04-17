using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private ParticleSystem HazardDeathParticles;
    
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        HazardDeathParticles.gameObject.SetActive(false);
    }
    
    public IEnumerator CueDeath()
    {
        HazardDeathParticles.transform.parent = null; //unparent the particles

        HazardDeathParticles.gameObject.SetActive(true);
        
        HazardDeathParticles.Play();

        GetComponent<Rigidbody2D>().isKinematic = true;

        yield return new WaitForSeconds(HazardDeathParticles.duration);
        
        gameManager.RespawnPlayers(); //respawn the player

        HazardDeathParticles.transform.parent = this.transform;

        GetComponent<Rigidbody2D>().isKinematic = false;
        yield return null;
    }
}
