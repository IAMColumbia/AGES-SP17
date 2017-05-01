using UnityEngine;
using System.Collections;

public class Cup : MonoBehaviour
{
    [SerializeField] GameObject cupAnchor;
    [SerializeField] GameObject cupTopCollider;

    [SerializeField] float randomShakeTimeMin = 0f;
    [SerializeField] float randomShakeTimeMax = 0.2f;
    //[SerializeField] float shakeTime = 0.2f;

    [SerializeField] float randomShakeSpeedMin = 100f;
    [SerializeField] float randomShakeSpeedMax = 300f;

    [SerializeField] float delayBeforeRotating = 3;
    [SerializeField] float delayBeforeResetting = 2;


    Rigidbody cupRigidbody;
    Vector3 initialPosition;
    Quaternion initialRotation;

    // Use this for initialization
    void Start ()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        cupRigidbody = GetComponent<Rigidbody>();

       StartCoroutine(Shake());

	}//end Start()
	
	// Update is called once per frame
	void Update ()
    {

	}//end Update()

    public IEnumerator Shake()
    {
        float randomShakeSpeed = Random.Range(randomShakeSpeedMin, randomShakeSpeedMax);
        float randomShakeTime = Random.Range(randomShakeTimeMin, randomShakeTimeMax);

        cupRigidbody.AddForce(Vector3.forward * randomShakeSpeed);
        print(randomShakeSpeed);

        yield return new WaitForSeconds(randomShakeTime);
        cupRigidbody.AddForce(Vector3.back * randomShakeSpeed);

        yield return new WaitForSeconds(randomShakeTime);
        cupRigidbody.AddForce(Vector3.left * randomShakeSpeed);

        yield return new WaitForSeconds(delayBeforeRotating);
        cupRigidbody.velocity = Vector3.zero;
        cupAnchor.GetComponent<ConfigurableJoint>().connectedBody = null;
        cupRigidbody.isKinematic = true;
        
        cupTopCollider.SetActive(false);
        StartCoroutine(RotateCup());

    } //end Coroutine Shake()

    void ResetCup()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        //ToDO: Put dice back in cup!
        cupTopCollider.SetActive(true);
        cupAnchor.GetComponent<ConfigurableJoint>().connectedBody = cupRigidbody;
        cupRigidbody.isKinematic = false;

    }//end ResetCup()

    public IEnumerator RotateCup()
    {
        float rotationTime = .5f;
        float rotationSpeed = 5;
        

        for (float i = 0; i < rotationTime; i+=Time.deltaTime)
        {
            transform.Rotate(0, 0, rotationSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeResetting);
        ResetCup();
        
    }//end Coroutine RotateCup()

}//end Cup Class

