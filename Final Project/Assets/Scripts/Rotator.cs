using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    [SerializeField]
    float xCoordinate, yCoordinate, zCoordinate, xSpeed, ySpeed, zSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(xCoordinate * xSpeed * Time.deltaTime, 
            yCoordinate * ySpeed * Time.deltaTime,
            zCoordinate * zSpeed * Time.deltaTime);
	}
}
