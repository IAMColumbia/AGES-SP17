using UnityEngine;
using System.Collections;

public class Sweep : MonoBehaviour {

    float initY;
    float t = 0;
    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime * speed;

        if(t > Mathf.PI * 2)
        {
            t = 0;
        }

        goToAngle(t);
	}

    void goToAngle(float theta)
    {
        float angle = theta * Mathf.Rad2Deg + initY;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
