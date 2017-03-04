using UnityEngine;
using System.Collections;

public interface IDamagable
{
    void TakeDamage(float damage, Vector3 explosionDirection);
}
