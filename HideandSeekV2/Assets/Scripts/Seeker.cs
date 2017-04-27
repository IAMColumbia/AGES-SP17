using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Seeker : MonoBehaviour {

    
    [SerializeField]
    public float forwardSpeed = 8f, TagDistance = 4f;
    [SerializeField]
    LayerMask tagLayer;
    GameManager GM;
    RigidbodyFirstPersonController RFPC;
    PlayerManager PM;
    int PlayerNumber;
 





	// Use this for initialization
	void Start () {

        GM = FindObjectOfType<GameManager>();

        RFPC = GetComponent<RigidbodyFirstPersonController>();
        tagLayer = 1 << 8 | 1 << 9 | 1 << 10 | 1 << 11;


    }

    void Update()
    {
        
        if (Input.GetButtonDown(RFPC.SubmitName))
        {
            Debug.Log(RFPC.SubmitName);
            GM.audioSource.clip = GM.audioClip[GM.AudioSourcetoPlay];
            GM.audioSource.volume = .25f;
            GM.audioSource.Play();
            Tag();
        }

    }

	
    void Tag()
    {
        Debug.Log("pressed Tag");
        RaycastHit Tagged;

        //Note  raycasting error to where the object is not always being checked for or Hit
        if (Physics.Raycast(transform.position, RFPC.cam.transform.forward, out Tagged, TagDistance, tagLayer))
        {
            if (Tagged.collider.gameObject.tag == "Hider")
            {
                
                            
               // Destroy(GM.playerTaggedParticle, 1f);
                GM.NumOfHiders--;
                Tagged.collider.gameObject.AddComponent<Spectator>();
                Tagged.collider.gameObject.GetComponent<RigidbodyFirstPersonController>().Cube.SetActive(false);
                Tagged.collider.gameObject.tag = "Spectator";
                Tagged.collider.gameObject.GetComponent<Hider>().enabled = false;

                

                
            }

            Debug.Log("Hit " + Tagged.collider.gameObject.name);
        }

    }


 
}
