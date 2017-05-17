using UnityEngine;
using System.Collections;

public class RotateWalls : MonoBehaviour {

    // Use this for initialization
    public Renderer rend;
    public float speed;
    public Vector3 direction;
    // Update is called once per frame
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update () {
        Rotation();
	}
    void Rotation()
    {
        Vector3 center = rend.bounds.center;
      //  transform.RotateAround(center, Vector3.up,1);

      transform.Rotate(direction * speed * Time.deltaTime, Space.World);
    }
   
}
