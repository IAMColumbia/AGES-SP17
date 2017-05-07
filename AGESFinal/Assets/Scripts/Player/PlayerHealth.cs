using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private ParticleSystem HazardDeathParticles;

    [SerializeField]
    private AudioSource deathSound;

    private int blinkTime = 8;

    private GameManager gameManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        deathSound = HazardDeathParticles.GetComponent<AudioSource>();

        HazardDeathParticles.gameObject.SetActive(false);
    }
    
    public void CueDeath()
    {
        HazardDeathParticles.transform.parent = null;
        HazardDeathParticles.gameObject.SetActive(true);

        HazardDeathParticles.Play();
        deathSound.Play();

        transform.parent.gameObject.SetActive(false);
        gameManager.StartCoroutine("RespawnPlayers");
        
    }

    public IEnumerator playerFlashAndDisableShooting()
    {
        gameObject.layer = 9;
        GetComponentInParent<SpriteRenderer>().color = new Color(255, 255, 255, .5f);
        GetComponentInParent<PlayerShooting>().enabled = false;

        for (int i = 0; i < blinkTime; i++)
        {
            yield return new WaitForSeconds(.1f);
            GetComponentInParent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            GetComponentInParent<SpriteRenderer>().enabled = true;
        }

        GetComponentInParent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        GetComponentInParent<PlayerShooting>().enabled = true;
        gameObject.layer = 10;
    }
}
