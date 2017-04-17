using UnityEngine;
using System.Collections;

public class Cup : MonoBehaviour
{
    //[SerializeField] 
    Rigidbody cupRigidbody;

	// Use this for initialization
	void Start ()
    {
        cupRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //RotateCup();
	}

    private void FixedUpdate()
    {
        cupRigidbody.AddRelativeForce(Vector3.forward * 10);
    }



    public void RotateCup()
    {
        transform.Rotate(0,0,120);
    }
}
