using UnityEngine;
using System.Collections;

public class ShootableObject : MonoBehaviour
{

    [SerializeField]
    MovingPlatform movingPlatform;

    [SerializeField]
    int newSpeed;

    public void Damage(int damageAmount)
    {    
        if (movingPlatform.speed > 0)
        {            
            movingPlatform.speed = 0;
        }
        else if (movingPlatform.speed <= 0)
        {
            movingPlatform.speed = newSpeed;
        }
    }
}