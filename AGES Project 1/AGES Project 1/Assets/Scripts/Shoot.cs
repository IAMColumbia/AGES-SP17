using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour 
{
	[SerializeField]
	Transform firingPoint;

	[SerializeField]
	GameObject box;

	[SerializeField]
	Quaternion pointRotation;

	private Vector3 pointPoint;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void FixedUpdate()
	{
		RaycastInAnalogStickDirection ();
	}

	void RaycastInAnalogStickDirection()
	{
		pointPoint = new Vector3 (firingPoint.transform.position.x, firingPoint.transform.position.y, firingPoint.transform.position.z);

		if (Input.GetButtonDown("Fire_GreenCar"))
		{
			Instantiate (box, pointPoint, pointRotation);
		}
	}
}
