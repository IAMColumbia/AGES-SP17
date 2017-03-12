using UnityEngine;
using System.Collections;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField]
    ParticleSystem fire;
    [SerializeField]
    float damageValue = 3;

    int tickCount;

    void Awake()
    {
        tickCount = 0;
        fire.Play();
        InvokeRepeating("DealDamage", 0, 0.5f);
    }

    void DealDamage()
    {
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = false;
        tickCount++;

        if (tickCount >= 6 && IsInvoking("DealDamage"))
        {
            CancelInvoke("DealDamage");
            fire.Stop();
            Destroy(gameObject, fire.duration);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<TakeDamage>())
        {
            collider.GetComponent<TakeDamage>().Damaged(damageValue);
        }
    }
}
