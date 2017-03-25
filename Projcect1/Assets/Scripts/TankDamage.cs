using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.Rigidbody))]
public class TankDamage : MonoBehaviour
{
    [SerializeField]
    private int playerDamage;
    [SerializeField]
    private Text damageText;
    [SerializeField]
    private string PlayerID;
    //please adjust damage multiplier, 100 is too much, 10 is still too little
    [SerializeField]
    private float damageMultiplier;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateDamageText();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UnityEngine.Rigidbody opponentRigidBody = collision.gameObject.GetComponent<UnityEngine.Rigidbody>();

            //detects contact points and direction to be launched in direction opposite of contact
            Vector3 launchDirection = new Vector3();
            launchDirection = collision.contacts[0].point - transform.position;
            launchDirection = -launchDirection.normalized;

            LaunchPlayer(playerDamage,collision.relativeVelocity.magnitude,launchDirection,opponentRigidBody);
            //TODO: Add Dynamic Values
            TakeDamage(3);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            //TODO: Have argument be taken from the Bullet, not hardcoded
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damageToTake)
    {
        playerDamage = playerDamage + damageToTake;
    }

    //PLEASE TEST ME
    private void LaunchPlayer(int damage, float impactForce, Vector3 direction, UnityEngine.Rigidbody objectToLaunch)
    {
        float launchForce = damage * impactForce * damageMultiplier;
        objectToLaunch.AddForce(direction * launchForce);
    }

    private void UpdateDamageText()
    {
        damageText.text = PlayerID + " Damage: " + playerDamage.ToString();
    }
}
