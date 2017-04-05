using UnityEngine;
using System.Collections;

public class Sweep : MonoBehaviour {

    float initY;
    public float t = 0;
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
        if(t < 0)
        {
            t = Mathf.PI * 2;
        }

        goToAngle(t - Mathf.PI / 2);
	}

    void goToAngle(float theta)
    {
        float angle = theta * Mathf.Rad2Deg + initY;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
