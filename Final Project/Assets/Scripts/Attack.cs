using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    int attackDamage;

	void Start()
    {
        attackDamage = this.gameObject.GetComponent<Health>().MaxStrength;
	}

    void DealDamage()
    {
        attackDamage = this.gameObject.GetComponent<Health>().MaxStrength;
    }
}
