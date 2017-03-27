using UnityEngine;
using System.Collections;
using System;

public class ArmorHealth : MonoBehaviour, IDamage
{
    [SerializeField]
    float armorHealth;

    public bool isNotBroken;

    Rigidbody rigidbody;

    // Use this for initialization
    void Start ()
    {
        armorHealth = 20;
        isNotBroken = true;
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.mass = 0;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    void FixedUpdate()
    {
        if (armorHealth <= 0 && isNotBroken)
        {
            GameObject clone = (GameObject)Instantiate(gameObject, transform.position, transform.rotation);
            clone.GetComponent<ArmorHealth>().isNotBroken = false;
            Rigidbody cloneRigidBody = clone.GetComponent<Rigidbody>();
            cloneRigidBody.mass = 1;
            cloneRigidBody.constraints = RigidbodyConstraints.None;
            isNotBroken = false;
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float x)
    {
        armorHealth = armorHealth - x;
    }
}
