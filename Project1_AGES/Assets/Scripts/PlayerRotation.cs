using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Quaternion currentRotation = transform.rotation;

        if (movement == new Vector3(0, 0, 0))
        {
            transform.rotation = currentRotation;
        }


        
        transform.rotation = Quaternion.LookRotation(movement);



    }
}
