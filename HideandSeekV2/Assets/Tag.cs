using UnityEngine;
using System.Collections;

public class Tag : MonoBehaviour {
    [SerializeField]
    float TagDistance;

    [SerializeField]
    Camera SeekerCam, Hider1Cam, Hider2Cam, Hider3Cam;
 
    // Use this for initialization
    void Start ()
    {
	

	}
	
	// Update is called once per frame
	void Update ()
            {
        #region Hide
        RaycastHit Tagged;

           
        if (Physics.Raycast(transform.position, Vector3.forward, out Tagged, TagDistance))
        {
            if (Tagged.collider.gameObject.tag == "Hider1")
            {
                Hider1Cam = SeekerCam;
                Debug.Log("Player Tagged");
                Destroy(Tagged.collider.gameObject, 1f);
            }

            if (Tagged.collider.gameObject.tag == "Hider2")
            {
                Hider2Cam = SeekerCam;
                Debug.Log("Player Tagged");
                Destroy(Tagged.collider.gameObject, 1f);
            }


            if (Tagged.collider.gameObject.tag == "Hider3")
            {
                Hider3Cam = SeekerCam;
                Debug.Log("Player Tagged");
                Destroy(Tagged.collider.gameObject, 1f);
            }
            Debug.Log("Hit " + Tagged.collider.gameObject.name);
        }
        #endregion




            }

    void HandleMovement ()
    {


    }
}
