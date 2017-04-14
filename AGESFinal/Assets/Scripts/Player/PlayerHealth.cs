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
        gameObject.SetActive(false);
        HazardDeathParticles.Play();
        gameManager.RespawnPlayers();
    }
}
