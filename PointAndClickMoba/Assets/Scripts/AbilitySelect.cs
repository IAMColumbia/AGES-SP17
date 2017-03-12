using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilitySelect : MonoBehaviour
{
    [SerializeField]
    AbilityCooldown abcd;
    [SerializeField]
    List<GameObject> abilities = new List<GameObject>();

    [HideInInspector]
    public GameObject chosenAbility1;
    [HideInInspector]
    public GameObject chosenAbility2;

    void Update()
    {
        ChangeAbility1(KeyCode.Alpha8, 0);
        ChangeAbility1(KeyCode.Alpha9, 1);
        ChangeAbility1(KeyCode.Alpha0, 2);

        ChangeAbility2(KeyCode.Alpha5, 0);
        ChangeAbility2(KeyCode.Alpha6, 1);
        ChangeAbility2(KeyCode.Alpha7, 2);
    }

    void ChangeAbility1(KeyCode key, int abilityNum)
    {
        if (Input.GetKeyDown(key))
        {
            chosenAbility1 = abilities[abilityNum];
        }
    }

    void ChangeAbility2(KeyCode key, int abilityNum)
    {
        if (Input.GetKeyDown(key))
        {
            chosenAbility2 = abilities[abilityNum];
        }
    }
}
