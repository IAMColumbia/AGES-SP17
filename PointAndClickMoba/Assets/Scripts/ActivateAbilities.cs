using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivateAbilities : MonoBehaviour
{
    [SerializeField]
    float flamethrowerCooldown;
    [SerializeField]
    GameObject flamethrowerPrefab;
    [SerializeField]
    KeyCode flamethrowerKey;
    [SerializeField]
    Slider flamethrowerCooldownSlider;
    [SerializeField]
    float lazerCooldown;
    [SerializeField]
    GameObject lazerPrefab;
    [SerializeField]
    KeyCode lazerKey;
    [SerializeField]
    Slider lazerCooldownSlider;
    [SerializeField]
    float bombCooldown;
    [SerializeField]
    GameObject bombPrefab;
    [SerializeField]
    KeyCode bombKey;
    [SerializeField]
    Slider bombCooldownSlider;

    float flamethrowerCooldownTimer;
    float lazerCooldownTimer;
    float bombCooldownTimer;
    float cooldownTimer = 0;

    [SerializeField]
    AbilitySelect activeAbility;

    void Update()
    {
        /*ActivateAbility(flamethrowerCooldownSlider, flamethrowerKey, flamethrowerPrefab, flamethrowerCooldown);
        ActivateAbility(lazerCooldownSlider, lazerKey, lazerPrefab, lazerCooldown);
        ActivateAbility(bombCooldownSlider, bombKey, bombPrefab, bombCooldown);*/

        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer == 0)
        {
            GameObject ability = Instantiate(activeAbility.chosenAbility1, transform.position, transform.rotation, transform) as GameObject;
        }
    }

    /*void ActivateAbility(Slider cooldownSlider, KeyCode activateKey, GameObject prefab, float cooldown)
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

        if (Input.GetKeyDown(activateKey) && cooldownTimer == 0)
        {
            GameObject ability = Instantiate(prefab, transform.position, transform.rotation, transform) as GameObject;
            cooldownTimer = cooldown;
            cooldownSlider.maxValue = cooldown;
        }
    }*/
}
