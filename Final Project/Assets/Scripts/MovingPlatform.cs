using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    int Xcoordinate, Ycoordinate, Zcoordinate;

    public float speed;

    private int direction = 1;

    Vector3 vector;

    void Start()
    {
        vector = new Vector3(Xcoordinate, Ycoordinate, Zcoordinate);
    }
    
	void Update ()
    {
        transform.Translate(vector * speed * direction * Time.deltaTime);
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Target")
        {
            if (direction == 1)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }
    }
}
