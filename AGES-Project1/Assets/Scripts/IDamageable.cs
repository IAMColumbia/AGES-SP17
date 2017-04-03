using UnityEngine;


public interface IDamageable<T>
{
    void TakeDamage(T damageTaken);
}
