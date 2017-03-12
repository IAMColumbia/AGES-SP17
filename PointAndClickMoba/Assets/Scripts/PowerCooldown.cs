using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerCooldown : MonoBehaviour
{
    [SerializeField]
    Slider cooldownSlider;
    [SerializeField]
    PowerSelect activePower;
    [SerializeField]
    Transform spawnLocation;

    [HideInInspector]
    public float cooldown;

    float cooldownTimer;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownSlider.value = cooldownTimer;
        }

        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }

        if (Input.GetButtonDown("") && cooldownTimer == 0)
        {
            GameObject power = Instantiate(activePower.chosenPower, spawnLocation.position, spawnLocation.rotation) as GameObject;
            cooldownTimer = cooldown;
            cooldownSlider.maxValue = cooldown;
        }
    }
}
