using UnityEngine;
using System.Collections;

public class EnemySquare : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        PsychoKiller();
	}

    void PsychoKiller()
    {
        if (gameObject.transform.position.y <= -5.8)
        {
            //Debug.Log("QU'EST QUE CE?");
            Destroy(gameObject);
        }
    }
}
