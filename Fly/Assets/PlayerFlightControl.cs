using UnityEngine;
using System.Collections;
using System;

public class PlayerFlightControl : MonoBehaviour {

    // Use this for initialization
    float moveHorizontal;
    float moveVertical;
	void Start () {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }
	
	// Update is called once per frame
	void Update () {

       
	}
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 alwaysMoving;

        alwaysMoving = Vector3.forward;
        transform.position = alwaysMoving;
        for (int i = 0; i < Time.fixedTime; i++)
        {
            while (i < Time.fixedTime)
            {
                transform.position = alwaysMoving;
            }
        }
    }
}
