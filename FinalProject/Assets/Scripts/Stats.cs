using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    [SerializeField]
    int maxStrength;
    [SerializeField]
    int maxArmor;

    int strength;
    int armor;

    public int Strength
    {
        get
        {
            return strength;
        }
    }

    public int Armor
    {
        get
        {
            return armor;
        }
    }

    void Start()
    {
        strength = maxStrength;
        armor = maxArmor;
    }
}
