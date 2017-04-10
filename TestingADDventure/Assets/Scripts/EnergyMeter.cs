using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
    public int startingEnergy;

    [HideInInspector]
    public int energyLeft;
    [HideInInspector]
    public bool canReduceEnergy; //{ get; set; }

    void Start()
    {
        //GetComponent<Slider>().maxValue = startingEnergy;
        canReduceEnergy = true;
        energyLeft = startingEnergy;
    }

    private void Update()
    {
        Debug.Log(energyLeft);
    }

    public void ReduceEnergy()
    {
        if (canReduceEnergy)
        {
            energyLeft--;
            //GetComponent<Slider>().value = energyLeft;
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
