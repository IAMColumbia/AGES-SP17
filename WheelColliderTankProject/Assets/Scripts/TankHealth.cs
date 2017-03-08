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
    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(DamageStates());
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

    IEnumerator DamageStates()
    {
            Debug.Log("Running");
            if (tankHealth <= 75)
            {
                Debug.Log("Light Damage!");
                StartCoroutine(LightDamage());
            }
            if (tankHealth <= 50)
            {
                StartCoroutine(MediumDamage());
            }
            if (tankHealth <= 25)
            {
                StartCoroutine(HeavyDamage());
            }
            if (tankHealth <= 0)
            {
                StartCoroutine(CriticalDamage());
            }
        
        yield return null;
    }
    IEnumerator LightDamage()
    {
        if (tankHealth <= 75)
        {
            Debug.Log("Light Damage!");
            damageSigns[0].SetActive(true);
        }
        
        yield return null;
    }

    IEnumerator MediumDamage()
    {
        damageSigns[1].SetActive(true);
        yield return null;
    }
    IEnumerator HeavyDamage()
    {
        damageSigns[2].SetActive(true);
        damageSigns[3].SetActive(true);

        yield return null;
    }
    IEnumerator CriticalDamage()
    {
        damageSigns[4].SetActive(true);
        damageSigns[5].SetActive(true);

        yield return new WaitForSeconds(2f);

        alive = false;
        Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(this.gameObject);
    }
}
