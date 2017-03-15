using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    [SerializeField]
    private float speed;

	// Use this for initialization
	void Start () {

        //this.transform.rotation = new Quaternion(0,0,0,0);
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Attack"))
        {
            if (!Input.GetButton("Shield"))
            {
                transform.Rotate(90, 0, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(90,0,0,1), Time.deltaTime * speed);
            }
            
        }
        if (Input.GetButtonUp("Attack"))
        {
            if (!Input.GetButton("Shield"))
            {
                //transform.Rotate(-90, 0, 0);
                //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(-90, 0, 0, 0), Time.deltaTime * speed);
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }


    }
}
