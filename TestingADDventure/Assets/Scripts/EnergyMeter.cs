using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
    public int startingEnergy;

    [HideInInspector]
    public int energyLeft;
    [HideInInspector]
    public bool canReduceEnergy;

    void Start()
    {
        canReduceEnergy = true;
        energyLeft = startingEnergy;
    }

    public void ReduceEnergy()
    {
        if (canReduceEnergy)
        {
            energyLeft--;
            canReduceEnergy = false;
            Invoke("ReduceEnergyTrue", 1);
        }

        if (energyLeft < 0)
        {
            energyLeft = 0;
        }
    }

    void ReduceEnergyTrue()
    {
        canReduceEnergy = true;
    }
}
