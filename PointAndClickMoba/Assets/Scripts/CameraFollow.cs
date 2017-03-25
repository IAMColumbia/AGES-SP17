using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    //Vector3 offset;

    void Start()
    {
        /*if (gameObject != null)
        {
            offset = transform.position - player.transform.position;
        }*/
    }

	void Update()
    {
        if (gameObject != null)
        {
            transform.position = player.transform.position/* + offset*/;
        }
	}
}
