using UnityEngine;
using System.Collections;

public class TimeBomb : MonoBehaviour {

    public float timeTilBakuretsu = 100f;
    public float explosionRadius = 5f;
    public LayerMask normalTimeLayer;
    public float explosionForce = 1000;

    public TextMesh text;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        timeTilBakuretsu--;

        text.text = "" + timeTilBakuretsu;

        if (timeTilBakuretsu < 0)
        {
            EXPLOSSSSSSSSSSSSSSION();
        }
    }

    private void EXPLOSSSSSSSSSSSSSSION()
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, normalTimeLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            if (!targetRigidbody)
                continue;

            if (targetRigidbody.gameObject.tag == "Player")
            {
                PlayerMovement pm = targetRigidbody.GetComponent<PlayerMovement>();
                pm.BlowUp();
            }
        }
        Destroy(gameObject);
    }
}
