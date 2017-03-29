using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup
{
    [SerializeField] float healAmount;

    public override bool OnCollected(TankShooting targetTank)
    {
        targetTank.gameObject.GetComponent<TankHealth>().TakeDamage(-healAmount);
        StartCoroutine(DeactivateSelf());

        return true;
    }
}
