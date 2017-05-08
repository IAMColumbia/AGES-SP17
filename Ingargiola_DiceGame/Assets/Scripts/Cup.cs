using UnityEngine;
using System.Collections;

public class Cup : Pawn
{
    [SerializeField]
    Pawn pawnReference;


    [Header("Cup Settings")]
    [SerializeField] GameObject cupAnchor;
    [SerializeField] GameObject cupTopCollider;
    Rigidbody cupRigidbody;
    Vector3 cupInitialPosition;
    Quaternion cupInitialRotation;

    [SerializeField] float randomShakeTimeMin = 0f;
    [SerializeField] float randomShakeTimeMax = 0.2f;
    //[SerializeField] float shakeTime = 0.2f;

    [SerializeField] float randomShakeSpeedMin = 100f;
    [SerializeField] float randomShakeSpeedMax = 300f;

    [SerializeField] float delayBeforeRotating = 3;
    [SerializeField] float delayBeforeResetting = 2;

    [SerializeField] string rollAxisName;
    [SerializeField] GameObject rollCamera;
    [SerializeField] GameObject diceGameObject;
    Vector3 dicePositionInsideCup;
    Quaternion diceRotationInsideCup;



    // Use this for initialization
    void Start()
    {
        cupInitialPosition = transform.position;
        //ahHaha = transform.position;
        cupInitialRotation = transform.rotation;
        cupRigidbody = GetComponent<Rigidbody>();

        //diceRotationInsideCup = diceGameObject.GetComponent<Transform>().rotation;
        dicePositionInsideCup = diceGameObject.GetComponent<Transform>().position;
        print("This is the dice's position inside the cup: "
                    + dicePositionInsideCup);


    }//end Start()

    // Update is called once per frame
    void Update()
    {
        InitiateDiceRoll();

    }//end Update()

    public void InitiateDiceRoll()
    {
        if (Input.GetButtonDown(rollAxisName))
        {
            pawnReference.diceRoll = 0;
            pawnReference.canPawnMove = true;
            pawnReference.canResetCup = false;
            rollCamera.SetActive(true);
            StartCoroutine(Shake());
        }
    }

    public IEnumerator Shake()
    {

        float randomShakeSpeed = Random.Range(randomShakeSpeedMin, randomShakeSpeedMax);
        float randomShakeTime = Random.Range(randomShakeTimeMin, randomShakeTimeMax);

        cupRigidbody.AddForce(Vector3.forward * randomShakeSpeed);
        print("Random Shake Speed: " + randomShakeSpeed);

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
        rollCamera.SetActive(false);

        transform.position = cupInitialPosition;
        transform.rotation = cupInitialRotation;
        diceGameObject.transform.position = dicePositionInsideCup;

        cupTopCollider.SetActive(true);
        cupAnchor.GetComponent<ConfigurableJoint>().connectedBody = cupRigidbody;
        cupRigidbody.isKinematic = false;
        print("can't move");
        pawnReference.canPawnMove = false;

    }//end ResetCup()

    public IEnumerator RotateCup()
    {
        float rotationTime = .5f;
        float rotationSpeed = 5;


        for (float i = 0; i < rotationTime; i += Time.deltaTime)
        {
            transform.Rotate(0, 0, rotationSpeed);
            yield return null;
        }

        yield return new WaitUntil(() => pawnReference.canResetCup == true);
        ResetCup();

    }//end Coroutine RotateCup()

}//end Cup Class





































//using UnityEngine;
//using System.Collections;

//public class Cup : MonoBehaviour
//{
//    [SerializeField] GameObject cupAnchor;
//    [SerializeField] GameObject cupTopCollider;

//    [SerializeField] float randomShakeTimeMin = 0f;
//    [SerializeField] float randomShakeTimeMax = 0.2f;
//    //[SerializeField] float shakeTime = 0.2f;

//    [SerializeField] float randomShakeSpeedMin = 100f;
//    [SerializeField] float randomShakeSpeedMax = 300f;

//    [SerializeField] float delayBeforeRotating = 3;
//    [SerializeField] float delayBeforeResetting = 2;


//    Rigidbody cupRigidbody;
//    Vector3 initialPosition;
//    Quaternion initialRotation;

//    // Use this for initialization
//    void Start ()
//    {
//        initialPosition = transform.position;
//        initialRotation = transform.rotation;
//        cupRigidbody = GetComponent<Rigidbody>();

//       StartCoroutine(Shake());

//	}//end Start()

//	// Update is called once per frame
//	void Update ()
//    {

//	}//end Update()

//    public IEnumerator Shake()
//    {
//        float randomShakeSpeed = Random.Range(randomShakeSpeedMin, randomShakeSpeedMax);
//        float randomShakeTime = Random.Range(randomShakeTimeMin, randomShakeTimeMax);

//        cupRigidbody.AddForce(Vector3.forward * randomShakeSpeed);
//        print(randomShakeSpeed);

//        yield return new WaitForSeconds(randomShakeTime);
//        cupRigidbody.AddForce(Vector3.back * randomShakeSpeed);

//        yield return new WaitForSeconds(randomShakeTime);
//        cupRigidbody.AddForce(Vector3.left * randomShakeSpeed);

//        yield return new WaitForSeconds(delayBeforeRotating);
//        cupRigidbody.velocity = Vector3.zero;
//        cupAnchor.GetComponent<ConfigurableJoint>().connectedBody = null;
//        cupRigidbody.isKinematic = true;

//        cupTopCollider.SetActive(false);
//        StartCoroutine(RotateCup());

//    } //end Coroutine Shake()

//    void ResetCup()
//    {
//        transform.position = initialPosition;
//        transform.rotation = initialRotation;
//        //ToDO: Put dice back in cup!
//        cupTopCollider.SetActive(true);
//        cupAnchor.GetComponent<ConfigurableJoint>().connectedBody = cupRigidbody;
//        cupRigidbody.isKinematic = false;

//    }//end ResetCup()

//    public IEnumerator RotateCup()
//    {
//        float rotationTime = .5f;
//        float rotationSpeed = 5;


//        for (float i = 0; i < rotationTime; i+=Time.deltaTime)
//        {
//            transform.Rotate(0, 0, rotationSpeed);
//            yield return null;
//        }

//        yield return new WaitForSeconds(delayBeforeResetting);
//        ResetCup();

//    }//end Coroutine RotateCup()

//}//end Cup Class

