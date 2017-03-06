using UnityEngine;
using System.Collections;
using System;

public interface IDamageable 
{
    event Action CriticalDamageReceived;
    void TakeDamage(float amount, string id, IDamageSource source);
}
