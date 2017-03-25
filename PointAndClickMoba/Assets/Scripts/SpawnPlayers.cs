using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField]
    GameObject tankPrefab;
    [SerializeField]
    GameObject cannonPrefab;
    [SerializeField]
    GameObject healthBarPrefab;

    AbilitySelect AbSelect;
    GameObject p1Tank;
    GameObject p2Tank;
    GameObject p3Tank;
    GameObject p4Tank;
    GameObject p1Cannon;
    GameObject p2Cannon;
    GameObject p3Cannon;
    GameObject p4Cannon;
    GameObject p1HealthBar;
    GameObject p2HealthBar;
    GameObject p3HealthBar;
    GameObject p4HealthBar;
    Transform p1Nozzle;
    Transform p2Nozzle;
    Transform p3Nozzle;
    Transform p4Nozzle;
    AbilityCooldown p1CooldownSlider;
    AbilityCooldown p2CooldownSlider;
    AbilityCooldown p3CooldownSlider;
    AbilityCooldown p4CooldownSlider;

    void Start()
    {
        AbSelect = GameObject.Find("AbilitySelector").GetComponent<AbilitySelect>();

        p1Tank = Instantiate(tankPrefab, new Vector3(-16, 0, -16), Quaternion.Euler(0, 45, 0)) as GameObject;
        p1Cannon = Instantiate(cannonPrefab, new Vector3(-16, 0, -16), Quaternion.Euler(0, 45, 0)) as GameObject;
        p1HealthBar = Instantiate(healthBarPrefab, new Vector3(-16, 0, -16), Quaternion.Euler(0, 0, 0)) as GameObject;
        p1Nozzle = p1Cannon.transform.GetChild(0).FindChild("CannonNozzle");
        p1CooldownSlider = p1HealthBar.transform.GetChild(0).GetComponentInChildren<AbilityCooldown>();
        p1Tank.GetComponent<PlayerMovement>().horizontalMoveInput = "P1MoveHorizontal";
        p1Tank.GetComponent<PlayerMovement>().verticalMoveInput = "P1MoveVertical";
        p1Tank.GetComponent<TakeDamage>().healthSlider = p1HealthBar.transform.GetChild(0).GetComponentInChildren<Slider>();
        p1Cannon.GetComponent<RotateTowardMouse>().horizontalLookInput = "P1LookHorizontal";
        p1Cannon.GetComponent<RotateTowardMouse>().verticalLookInput = "P1LookVertical";
        p1Cannon.GetComponent<CameraFollow>().player = p1Tank;
        p1HealthBar.GetComponent<CameraFollow>().player = p1Tank;
        p1CooldownSlider.playerNumber = 1;
        p1CooldownSlider.spawnLocation = p1Nozzle;
        p1CooldownSlider.activateButton1 = "P1Ability1";
        p1CooldownSlider.activateButton2 = "P1Ability2";

        p2Tank = Instantiate(tankPrefab, new Vector3(16, 0, 16), Quaternion.Euler(0, -135, 0)) as GameObject;
        p2Cannon = Instantiate(cannonPrefab, new Vector3(16, 0, 16), Quaternion.Euler(0, -135, 0)) as GameObject;
        p2HealthBar = Instantiate(healthBarPrefab, new Vector3(16, 0, 16), Quaternion.Euler(0, 0, 0)) as GameObject;
        p2Nozzle = p2Cannon.transform.GetChild(0).FindChild("CannonNozzle");
        p2CooldownSlider = p2HealthBar.transform.GetChild(0).GetComponentInChildren<AbilityCooldown>();
        p2Tank.GetComponent<PlayerMovement>().horizontalMoveInput = "P2MoveHorizontal";
        p2Tank.GetComponent<PlayerMovement>().verticalMoveInput = "P2MoveVertical";
        p2Tank.GetComponent<TakeDamage>().healthSlider = p2HealthBar.transform.GetChild(0).GetComponentInChildren<Slider>();
        p2Cannon.GetComponent<RotateTowardMouse>().horizontalLookInput = "P2LookHorizontal";
        p2Cannon.GetComponent<RotateTowardMouse>().verticalLookInput = "P2LookVertical";
        p2Cannon.GetComponent<CameraFollow>().player = p2Tank;
        p2HealthBar.GetComponent<CameraFollow>().player = p2Tank;
        p2CooldownSlider.playerNumber = 2;
        p2CooldownSlider.spawnLocation = p2Nozzle;
        p2CooldownSlider.activateButton1 = "P2Ability1";
        p2CooldownSlider.activateButton2 = "P2Ability2";

        if (AbSelect.numberOfPlayers == 3 || AbSelect.numberOfPlayers == 4)
        {
            p3Tank = Instantiate(tankPrefab, new Vector3(-16, 0, 16), Quaternion.Euler(0, 135, 0)) as GameObject;
            p3Cannon = Instantiate(cannonPrefab, new Vector3(-16, 0, 16), Quaternion.Euler(0, 135, 0)) as GameObject;
            p3HealthBar = Instantiate(healthBarPrefab, new Vector3(-16, 0, 16), Quaternion.Euler(0, 0, 0)) as GameObject;
            p3Nozzle = p3Cannon.transform.GetChild(0).FindChild("CannonNozzle");
            p3CooldownSlider = p3HealthBar.transform.GetChild(0).GetComponentInChildren<AbilityCooldown>();
            p3Tank.GetComponent<PlayerMovement>().horizontalMoveInput = "P3MoveHorizontal";
            p3Tank.GetComponent<PlayerMovement>().verticalMoveInput = "P3MoveVertical";
            p3Tank.GetComponent<TakeDamage>().healthSlider = p3HealthBar.transform.GetChild(0).GetComponentInChildren<Slider>();
            p3Cannon.GetComponent<RotateTowardMouse>().horizontalLookInput = "P3LookHorizontal";
            p3Cannon.GetComponent<RotateTowardMouse>().verticalLookInput = "P3LookVertical";
            p3Cannon.GetComponent<CameraFollow>().player = p3Tank;
            p3HealthBar.GetComponent<CameraFollow>().player = p3Tank;
            p3CooldownSlider.playerNumber = 3;
            p3CooldownSlider.spawnLocation = p3Nozzle;
            p3CooldownSlider.activateButton1 = "P3Ability1";
            p3CooldownSlider.activateButton2 = "P3Ability2";
        }

        if (AbSelect.numberOfPlayers == 4)
        {
            p4Tank = Instantiate(tankPrefab, new Vector3(16, 0, -16), Quaternion.Euler(0, -45, 0)) as GameObject;
            p4Cannon = Instantiate(cannonPrefab, new Vector3(16, 0, -16), Quaternion.Euler(0, -45, 0)) as GameObject;
            p4HealthBar = Instantiate(healthBarPrefab, new Vector3(16, 0, -16), Quaternion.Euler(0, 0, 0)) as GameObject;
            p4Nozzle = p4Cannon.transform.GetChild(0).FindChild("CannonNozzle");
            p4CooldownSlider = p4HealthBar.transform.GetChild(0).GetComponentInChildren<AbilityCooldown>();
            p4Tank.GetComponent<PlayerMovement>().horizontalMoveInput = "P4MoveHorizontal";
            p4Tank.GetComponent<PlayerMovement>().verticalMoveInput = "P4MoveVertical";
            p4Tank.GetComponent<TakeDamage>().healthSlider = p4HealthBar.transform.GetChild(0).GetComponentInChildren<Slider>();
            p4Cannon.GetComponent<RotateTowardMouse>().horizontalLookInput = "P4LookHorizontal";
            p4Cannon.GetComponent<RotateTowardMouse>().verticalLookInput = "P4LookVertical";
            p4Cannon.GetComponent<CameraFollow>().player = p4Tank;
            p4HealthBar.GetComponent<CameraFollow>().player = p4Tank;
            p4CooldownSlider.playerNumber = 4;
            p4CooldownSlider.spawnLocation = p4Nozzle;
            p4CooldownSlider.activateButton1 = "P4Ability1";
            p4CooldownSlider.activateButton2 = "P4Ability2";
        }
    }
}
