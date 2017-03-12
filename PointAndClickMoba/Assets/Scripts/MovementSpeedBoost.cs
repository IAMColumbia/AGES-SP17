using UnityEngine;
using System.Collections;
using System;

public class MovementSpeedBoost : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    void Awake()
    {
        player.speedBoostAmount = 3;
        Invoke("EndSpeedBoost", 4);
    }

    void EndSpeedBoost()
    {
        player.speedBoostAmount = 0;
        Destroy(gameObject);
    }
}
