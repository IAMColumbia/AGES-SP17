using UnityEngine;
using System.Collections;

public class TankHealth : MonoBehaviour, IDamageable<float> {
    //I am having trouble understanding coroutines so I have just finished the project objectives however i know I have not done them properly yet.
    [SerializeField]
    float tankHealth = 100;

    [SerializeField]
    GameObject[] damageSigns;

    [SerializeField]
    GameObject explosionPrefab;

    bool alive;
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < damageSigns.Length; i++)
        {
            damageSigns[i].SetActive(false);
        }
        //StartCoroutine("Killed");
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        VisualizeDamage();
	}
    public void TakeDamage(float damageTaken)
    {
        damageTaken = 10;
        tankHealth = tankHealth - damageTaken;

        
    }
    void OnTriggerEnter(Collider other)
    {
        TakeDamage(tankHealth);
    }

    void VisualizeDamage()
    {
        
            if (tankHealth <= 80)
            {
                damageSigns[0].SetActive(true);
            }
            if (tankHealth <= 60)
            {
                damageSigns[1].SetActive(true);
            }
            if (tankHealth <= 50)
            {
                damageSigns[2].SetActive(true);
            }
            if (tankHealth <= 30)
            {
                damageSigns[3].SetActive(true);
                damageSigns[4].SetActive(true);
            }
            if (tankHealth <= 20)
            {
                damageSigns[5].SetActive(true);
            }
        if (tankHealth == 0)
        {
            alive = false;
            Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(this.gameObject);
        }

    }

   
    
}
