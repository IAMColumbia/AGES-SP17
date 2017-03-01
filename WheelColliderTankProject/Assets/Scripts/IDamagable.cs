using UnityEngine;
using System;
using System.Collections.Generic;

public interface IDamagable<T>
{
    void TakeDamage(T damageTaken);
}
