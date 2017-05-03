using UnityEngine;
using System.Collections;

public class RingScript : MonoBehaviour {

    // Use this for initialization
 
    [SerializeField]
    GameObject ring;  
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0);
            ring.SetActive(false);        //  SceneManager.LoadScene(sceneToStart);
        }      
    }
   

}
