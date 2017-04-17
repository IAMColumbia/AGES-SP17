using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private ParticleSystem HazardDeathParticles;
    
    private GameManager gameManager;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void CueHazardDeathParticles()
    {
        HazardDeathParticles.transform.parent = null; //unparent the particles

        gameObject.SetActive(false);

        HazardDeathParticles.Play();

        Destroy(HazardDeathParticles.gameObject, HazardDeathParticles.duration); //destroy them after their done

        gameManager.RespawnPlayers(); //respawn the player

    }
}
