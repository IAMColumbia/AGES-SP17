using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class TankDamage : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private int playerDamage;
    [SerializeField]
    private Text damageText;
    [SerializeField]
    private string PlayerID;
    //please adjust damage multiplier, 100 is too much, 10 is still too little
    [SerializeField]
    private float damageMultiplier;
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private AudioSource explosionSound;
    #endregion

    // Update is called once per frame
    void Update ()
    {
        UpdateDamageText();
	}

    //TODO: Don't make all players involved take same damage
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody opponentRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            //detects contact points and direction to be launched in direction opposite of contact
            Vector3 launchDirection = new Vector3();
            launchDirection = collision.contacts[0].point - transform.position;
            launchDirection = -launchDirection.normalized;

            LaunchPlayer(playerDamage,collision.relativeVelocity.magnitude,launchDirection,opponentRigidBody);
            int impactForceInt = (int)collision.relativeVelocity.magnitude;
            TakeDamage(impactForceInt);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        }
    }

    //Used to take damage from everything from bullets to collision
    private void TakeDamage(int damageToTake)
    {
        playerDamage = playerDamage + damageToTake;
    }

    //Used to exaggerate collision forces
    private void LaunchPlayer(int damage, float impactForce, Vector3 direction, Rigidbody objectToLaunch)
    {
        float launchForce = damage * impactForce * damageMultiplier;
        objectToLaunch.AddForce(direction * launchForce);
    }

    private void UpdateDamageText()
    {
        damageText.text = PlayerID + " Damage: " + playerDamage.ToString();
    }

    public void Explode()
    {
        explosion.transform.parent = null;
        explosionSound.Play();
        explosion.Play();
        damageText.text = PlayerID + " : DEAD";
    }
}
