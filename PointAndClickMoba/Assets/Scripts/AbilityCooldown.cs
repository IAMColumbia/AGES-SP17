using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField]
    Slider cooldownSlider;
    [SerializeField]
    AbilitySelect activeAbility;
    [SerializeField]
    Transform spawnLocation;
    [SerializeField]
    string activateButton1;
    [SerializeField]
    string activateButton2;

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

        if (Input.GetButtonDown(activateButton1) && cooldownTimer == 0)
        {
            GameObject ability1 = Instantiate(activeAbility.chosenAbility1, spawnLocation.position, spawnLocation.rotation, spawnLocation) as GameObject;
            cooldownTimer = ability1.GetComponent<AbilityDetails>().cooldown;
            cooldownSlider.maxValue = ability1.GetComponent<AbilityDetails>().cooldown;
        }
        
        if (Input.GetButtonDown(activateButton2) && cooldownTimer == 0)
        {
            GameObject ability2 = Instantiate(activeAbility.chosenAbility2, spawnLocation.position, spawnLocation.rotation, spawnLocation) as GameObject;
            cooldownTimer = ability2.GetComponent<AbilityDetails>().cooldown;
            cooldownSlider.maxValue = ability2.GetComponent<AbilityDetails>().cooldown;
        }
    }
}
