using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    int playerNumber;

    [HideInInspector]
    public float cooldown;

    float cooldownTimer;
    GameObject selectedAbility1;
    GameObject selectedAbility2;

    void Start()
    {
        activeAbility = GameObject.Find("AbilitySelector").GetComponent<AbilitySelect>();

        switch (playerNumber)
        {
            case 1:
                selectedAbility1 = activeAbility.P1chosenAbility1;
                selectedAbility2 = activeAbility.P1chosenAbility2;
                break;
            case 2:
                selectedAbility1 = activeAbility.P2chosenAbility1;
                selectedAbility2 = activeAbility.P2chosenAbility2;
                break;
            case 3:
                selectedAbility1 = activeAbility.P3chosenAbility1;
                selectedAbility2 = activeAbility.P3chosenAbility2;
                break;
            case 4:
                selectedAbility1 = activeAbility.P4chosenAbility1;
                selectedAbility2 = activeAbility.P4chosenAbility2;
                break;
            default:
                break;
        }
    }

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
            GameObject ability1 = Instantiate(selectedAbility1, spawnLocation.position, spawnLocation.rotation, spawnLocation) as GameObject;
            cooldownTimer = ability1.GetComponent<AbilityDetails>().cooldown;
            cooldownSlider.maxValue = ability1.GetComponent<AbilityDetails>().cooldown;
        }
        
        if (Input.GetButtonDown(activateButton2) && cooldownTimer == 0)
        {
            GameObject ability2 = Instantiate(selectedAbility2, spawnLocation.position, spawnLocation.rotation, spawnLocation) as GameObject;
            cooldownTimer = ability2.GetComponent<AbilityDetails>().cooldown;
            cooldownSlider.maxValue = ability2.GetComponent<AbilityDetails>().cooldown;
        }
    }
}
