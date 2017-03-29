using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;
	[SerializeField]
	private float boostSpeed;
    [SerializeField]
    private string playerNumber;
    private float startingSpeed;
	private Quaternion startingRotation;
    public bool canPlayerMove;

	public GameObject PlayerTrigger;
	public GameObject speedTrailParticleSystem;

	// Use this for initialization
	void Start () {
	
		startingSpeed = speed;
		startingRotation = gameObject.transform.rotation;
        canPlayerMove = false;

	}
	
	// Update is called once per frame
	void Update () {

		bool poweredUp = PlayerTrigger.GetComponent<PickUp>().poweredUp;

		if (poweredUp == true) 
		{
			speed = boostSpeed;
			speedTrailParticleSystem.SetActive(true);
		} 

		if(poweredUp == false)
		{
			speed = startingSpeed;
			speedTrailParticleSystem.SetActive(false);
		}

        if (canPlayerMove == true)
        {
            float dirX = Input.GetAxis("Horizontal" + playerNumber) * speed;
            float dirZ = Input.GetAxis("Vertical" + playerNumber) * speed;
            transform.Translate(dirX, 0, dirZ);
        }

        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.rotation = Quaternion.LookRotation(movement);

		if(gameObject.transform.rotation != startingRotation)
		{
			gameObject.transform.rotation = startingRotation;
		}
        
	
	}

    void FixedUpdate()
    {

    }
}
