using UnityEngine;
using System.Collections;

public class Tag : MonoBehaviour {
    [SerializeField]
    float TagDistance;
    [SerializeField]
    LayerMask checkLayers;
    [SerializeField]
    Camera playerCamera;

    // Use this for initialization
    void Start ()
    {
	    
        
	}
	
	// Update is called once per frame
	void Update ()
            {
        #region Hide
        RaycastHit Tagged;

           //Note  raycasting error to where the object is not always being checked for or Hit
        if (Physics.Raycast(transform.position, playerCamera.transform.forward, out Tagged, TagDistance,checkLayers))
        { 
            if (Tagged.collider.gameObject.tag == "Hider")
            {                
                Debug.Log("Player Tagged");
                Tagged.collider.gameObject.GetComponent<PlayerScript>().isSpectating = true;
                Tagged.collider.gameObject.SetActive(false);
            }

            Debug.Log("Hit " + Tagged.collider.gameObject.name);
        }
        #endregion




        }


}
