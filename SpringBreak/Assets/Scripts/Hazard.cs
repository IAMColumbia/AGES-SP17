using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    GameObject player;
    [SerializeField]
    public float waterSpeed = 0.1f;

    [SerializeField]
    public GameObject waterPlane;

    [SerializeField]
    public GameObject goalSphereToggle;

   
    // public int sceneToStart = 2;
    public bool HasGoal
    {
        get
        {
            return goalSphereToggle.activeSelf;

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {          
           other.gameObject.SetActive(false);
                    
        //    SceneManager.LoadScene(sceneToStart);
        }
        if (other.gameObject.tag == "Stop")
        {
            transform.Translate(Vector3.up * 0 * Time.deltaTime, Space.World);
        }
    }
    public void WaterRise()
    {
        waterSpeed = 0.03f;
        waterPlane.transform.Translate(Vector3.up * waterSpeed * Time.deltaTime, Space.World);
        if (HasGoal == true)
        {
            waterPlane.transform.Translate(Vector3.up);

        }
        if (waterPlane.transform.position.y >= 15)
        {
            waterPlane.transform.Translate(Vector3.back);
        }
    }
    public void WaterDescend()
    {
        waterSpeed = 1.5f;
        waterPlane.transform.Translate(Vector3.down * waterSpeed, Space.World);

    }
}