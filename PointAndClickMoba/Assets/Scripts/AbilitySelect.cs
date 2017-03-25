using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilitySelect : MonoBehaviour
{
    public List<GameObject> abilities = new List<GameObject>();
    [HideInInspector]
    public GameObject P1chosenAbility1;
    [HideInInspector]
    public GameObject P1chosenAbility2;
    [HideInInspector]
    public GameObject P2chosenAbility1;
    [HideInInspector]
    public GameObject P2chosenAbility2;
    [HideInInspector]
    public GameObject P3chosenAbility1;
    [HideInInspector]
    public GameObject P3chosenAbility2;
    [HideInInspector]
    public GameObject P4chosenAbility1;
    [HideInInspector]
    public GameObject P4chosenAbility2;
    [HideInInspector]
    public int numberOfPlayers;

    void Update()
    {
        /*ChangeAbility1(KeyCode.Alpha8, 0);
        ChangeAbility1(KeyCode.Alpha9, 1);
        ChangeAbility1(KeyCode.Alpha0, 2);

        ChangeAbility2(KeyCode.Alpha5, 0);
        ChangeAbility2(KeyCode.Alpha6, 1);
        ChangeAbility2(KeyCode.Alpha7, 2);*/
    }

    void ChangeAbility1(KeyCode key, int abilityNum)
    {
        if (Input.GetKeyDown(key))
        {
            P1chosenAbility1 = abilities[abilityNum];
        }
    }

    void ChangeAbility2(KeyCode key, int abilityNum)
    {
        if (Input.GetKeyDown(key))
        {
            P1chosenAbility2 = abilities[abilityNum];
        }
    }
}
