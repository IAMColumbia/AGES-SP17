using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float dirX = Input.GetAxis("Horizontal") * speed;
        float dirZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.rotation = Quaternion.LookRotation(movement);


        
        transform.Translate(dirX,0,dirZ);
	
	}

    void FixedUpdate()
    {

    }
}
