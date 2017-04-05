using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField]
    int maxStrength = 10;
    [SerializeField]
    int maxArmor = 15;

    int currentStrength;
    int currentArmor;
    
    public int MaxStrength
    {
        get
        {
            return maxStrength;
        }
    }

    void Start()
    {
        currentStrength = maxStrength;
        currentArmor = maxArmor;
	}

    void TakeDamage(int damageAmount)
    {
        if (damageAmount > currentArmor)
        {
            damageAmount -= currentArmor;
            currentArmor = 0;

            currentStrength -= damageAmount;
        }
        else if (damageAmount == currentArmor)
        {
            currentArmor = 0;
        }
        else
        {
            damageAmount -= currentArmor;
        }

        if (currentStrength <= 0)
        {
            Die();
        }
    }

    //TODO: die method
    void Die()
    {
        Destroy(this.gameObject);
    }
}
