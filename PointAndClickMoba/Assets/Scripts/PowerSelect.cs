using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerSelect : MonoBehaviour
{
    [SerializeField]
    PowerCooldown pwcd;
    [SerializeField]
    List<GameObject> powers = new List<GameObject>();

    [HideInInspector]
    public GameObject chosenPower;

    void Update()
    {
        ChangeAbility(KeyCode.Alpha5, 0, 5);
        ChangeAbility(KeyCode.Alpha6, 1, 5);
        ChangeAbility(KeyCode.Alpha7, 2, 5);
    }

    void ChangeAbility(KeyCode key, int powerNum, float cd)
    {
        if (Input.GetKeyDown(key))
        {
            chosenPower = powers[powerNum];
            pwcd.cooldown = cd;
        }
    }
}
