using UnityEngine;
using System.Collections;

public class Cup : MonoBehaviour
{
    //[SerializeField] 
    Rigidbody cupRigidbody;
    [SerializeField] GameObject cupAnchor;
    [SerializeField] GameObject cupTopCollider;

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

        yield return new WaitForSeconds(1);
        cupRigidbody.velocity = Vector3.zero;
        cupAnchor.SetActive(false);
        RotateCup();


    }

    //rotatecup as a coroutine that checks for what the Z-eulerangle is
    //while its less than 120 keep rotating
    public void RotateCup()
    {
        //rotate on Z
        //transform.Rotate(0,0,120);
        //if z variable
       
        cupRigidbody.transform.Rotate(0, 0, 120);
        Debug.Log(cupRigidbody.gameObject.transform.eulerAngles.z);
        cupTopCollider.SetActive(false);
        
    }
}



/*
 select infventory object and add homeobjectscript to the inventory object once it is in the house
 if(gameObject.GetComponent<Pawn>() == null)   (could be in start to check if GameObject is set up correctly)
        Pawn newPawn = gameObject.AddComponent<Pawn>();
else
        rest of script
    
*/
