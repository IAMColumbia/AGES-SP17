using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private string shootButton;
    [SerializeField]
    private GameObject bullet;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Shoot();
	}

    void Shoot()
    {
        if (Input.GetButtonDown(shootButton))
        {
            Debug.Log("Shot fired!");
            GameObject bulletInstance = Instantiate(bullet) as GameObject;
            bulletInstance.gameObject.transform.Translate(gameObject.transform.forward);
        }
    }
}
