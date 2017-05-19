using UnityEngine;
using System.Collections;
using System;

public class RisingPlane : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject shark;

    [SerializeField]
    int speed = 5;

    float minTime = 2f;

    float maxTime = 10f;

    [SerializeField]
    float timer = 3;

  
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        checkPlayerLocation();

        timer -= Time.deltaTime;
        Debug.Log("Timer: " + timer);

   
        if (timer <= 0)
        {
            risingWater();
            timer = 5;
        }
        if(player.transform.position.y <= transform.position.y)
        {
           // lightSwitch.light.enabled = false;
        }
        else
        {
       //     lightSwitch.enabled = true;
        }
	}

    private void risingWater()
    {              
          // here if you want to change location then you can use MoveObject(); and delete the enabled thing.                                     
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        Debug.Log("Water is rising!!");
    }
        
    private void checkPlayerLocation()
    {
      if(player.transform.position.y <= transform.position.y)
        {
            //shark attack player
            //scary music play 
            //Play splash sound effects (Gurgles, gasps for air)
            
        }
    }
}
