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
        HazardDeathParticles.transform.parent = null;

        HazardDeathParticles.Play();

        Destroy(HazardDeathParticles.gameObject, HazardDeathParticles.duration);

        gameManager.RespawnPlayers();

    }
}
