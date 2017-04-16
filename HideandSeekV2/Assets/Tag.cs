using UnityEngine;
using System.Collections;

public class Tag : MonoBehaviour {
    [SerializeField]
    float TagDistance;
 
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
            if (Tagged.collider.gameObject.tag == "Hider")
            {                
                Debug.Log("Player Tagged");
                Destroy(Tagged.collider.gameObject, 1f); //Destroys Hider After 1 Second
            }

            Debug.Log("Hit " + Tagged.collider.gameObject.name);
        }
        #endregion




            }


}
