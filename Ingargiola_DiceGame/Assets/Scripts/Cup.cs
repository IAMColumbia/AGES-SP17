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

        StartCoroutine(Shake());
	}
	
	// Update is called once per frame
	void Update ()
    {
        //RotateCup();
	}

    private void FixedUpdate()
    {
    }

    public IEnumerator Shake()
    {

        //multiply by random
        //different direction by random amount


        float randomShakeSpeed = Random.Range(50f, 100f);
        float randomShakeTime = Random.Range(0f, 0.2f);

        cupRigidbody.AddForce(Vector3.forward * randomShakeSpeed);
        print(randomShakeSpeed);

        //TODO: should the time be random??
        yield return new WaitForSeconds(randomShakeTime);
        cupRigidbody.AddForce(Vector3.back * randomShakeSpeed);

        yield return new WaitForSeconds(randomShakeTime);
        cupRigidbody.AddForce(Vector3.left * randomShakeSpeed);

        yield return new WaitForSeconds(10);
        //RotateCup();


    }


    public void RotateCup()
    {
        //rotate on Z
        //transform.Rotate(0,0,120);

        cupRigidbody.transform.Rotate(0,0,120);
    }
}
