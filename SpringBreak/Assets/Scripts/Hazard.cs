using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    GameObject player;

    [SerializeField]
    int speed = 5;

    // public int sceneToStart = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {          
           player.SetActive(false);         
        //    SceneManager.LoadScene(sceneToStart);
        }
        if (other.gameObject.tag == "Stop")
        {
            speed = 0;
        }
    }
    public void WaterRise()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        speed = 5;
    }
    public void WaterDescend()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        speed = 10;      
    }
}