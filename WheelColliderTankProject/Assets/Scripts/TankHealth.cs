using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamagable {
    public enum DamageStatus { none, light, medium, heavy, critical};
    [SerializeField]
    GameObject[] damageIndicatorPrefabs;
    [SerializeField]
    int maxHealth;
    [SerializeField]
    Transform vfxSpawnPoint;
    DamageStatus damageStatus;

    [SerializeField]
    int currentHealth;

    GameObject showDamage;

    public void Start() {
        currentHealth = maxHealth;
        damageStatus = DamageStatus.none;
        showDamage = new GameObject();
    }

    public void DoDamage(int damageAmount) {
        currentHealth -= damageAmount;
        Debug.Log("Health = " + currentHealth);
        IncrimentDamageStatus();
    }

    private void IncrimentDamageStatus() {
        damageStatus++;
        
        switch (damageStatus)
        {
            case DamageStatus.light:
                ShowDamageLevel(0);
                break;
            case DamageStatus.medium:
                ShowDamageLevel(1);
                break;
            case DamageStatus.heavy:
                ShowDamageLevel(2);
                break;
            case DamageStatus.critical:
                ShowDamageLevel(3);
                DeActivateTankScripts();
                break;
            default:
                break;
        }
    }

    private void ShowDamageLevel(int i) {
        showDamage = Instantiate(damageIndicatorPrefabs[i]) as GameObject;
        showDamage.transform.parent = vfxSpawnPoint;
        showDamage.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void DeActivateTankScripts() {
        transform.gameObject.GetComponent<TankController>().enabled = false;
        transform.gameObject.GetComponent<TankShooting>().enabled = false;
        transform.gameObject.GetComponentInChildren<TankTurret>().enabled = false;
    }
}
