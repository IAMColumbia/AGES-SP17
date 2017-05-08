using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    [SerializeField]
    float rotationSpeed;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        float speed = (Time.deltaTime * rotationSpeed);
        transform.Rotate(new Vector3(0, 0, 1) * speed);
    }
}
