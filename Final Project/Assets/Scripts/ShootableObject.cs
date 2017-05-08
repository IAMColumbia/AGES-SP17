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
        if (movingPlatform.OriginalSpeed > 0)
        {            
            movingPlatform.OriginalSpeed = 0;
        }
        else if (movingPlatform.OriginalSpeed <= 0)
        {
            movingPlatform.OriginalSpeed = newSpeed;
        }
    }
}