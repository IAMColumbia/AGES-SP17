using UnityEngine;
using System.Collections;
using System;

public class MovementSpeedBoost : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    void Awake()
    {
        Invoke("EndSpeedBoost", 4);
    }

    void EndSpeedBoost()
    {
        Destroy(gameObject);
    }
}
