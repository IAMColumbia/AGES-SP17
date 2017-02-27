using UnityEngine;
using System.Collections;

public class BulletPickup : Pickup
{
    public override bool OnCollected(TankShooting targetTank)
    {
        if (targetTank.numberOfBullets < targetTank.MaxNumberOfBullets)
        {
            targetTank.numberOfBullets++;
            return true;
        }

        return false;
    }
}
