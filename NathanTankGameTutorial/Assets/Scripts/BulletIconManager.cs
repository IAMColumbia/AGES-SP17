using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletIconManager : MonoBehaviour
{
    [SerializeField] TankShooting tank;
    [SerializeField] List<Transform> icons;
    int activeBullets;
    float rotationSpeed = 100f;

	// Use this for initialization
	void Start ()
    {
	    foreach(Transform icon in icons)
        {
            icon.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //rotate
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        activeBullets = tank.numberOfBullets-1;


            for(int x = 0; x < icons.Count; x++)
            {
                if(x <= activeBullets)
                {
                    icons[x].gameObject.SetActive(true);
                }
                else
                {
                    icons[x].gameObject.SetActive(false);
                }
            }
	}
}
