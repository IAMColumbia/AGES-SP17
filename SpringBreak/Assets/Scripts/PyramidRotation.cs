using UnityEngine;
using System.Collections;
using System;

public class PyramidRotation : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject Center;
    [SerializeField]
    GameObject topCenter;
    [SerializeField]
    float rotationDamping;
    float step;

    Quaternion maxRotation;
    Quaternion minRotation;

    void Start()
    {    
        step = rotationDamping * Time.deltaTime;
        minRotation = Quaternion.Euler(-360, 0, -360);
        maxRotation = Quaternion.Euler(360, 0, 360);

    }
    void Update ()
    {
        stageMotion();
        CheckPosition();
        CheckRotation();
    }

    void CheckRotation()
    {
        Quaternion rotation = Quaternion.LookRotation(topCenter.transform.position - transform.position);
        if (transform.rotation.x <= -15 || transform.rotation.x >= 15)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
            transform.rotation = Quaternion.Slerp(minRotation, maxRotation, step * Time.deltaTime);
          //  transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        }
        else if (transform.rotation.z <= -15 || transform.rotation.z >= 15)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
            transform.rotation = Quaternion.Slerp(minRotation, maxRotation, step * Time.deltaTime);
           // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        }
        else if (transform.rotation.y <= -1 || transform.rotation.y >= 1)
            {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        }
    }
     private void stageMotion()
    {
        transform.Translate(Vector3.u
            p* step * Time.deltaTime);
    }
     void CheckPosition()
    {
       
        if (transform.position.x >= 5 || transform.position.x <= 5)
        {
            controlPositionX();
        }
        else if (transform.position.y >= 3 || transform.position.y <= -3)
        {
            controlPositionY();
        }
        else if (transform.position.z >= 3 || transform.position.z <= -3)
        {
            controlPositionZ();
        }
    }
     void controlPositionX()
    {
        transform.position = Vector3.MoveTowards(transform.position, Center.transform.position, step);
    }
    void controlPositionY()
    {
        transform.position = Vector3.MoveTowards(transform.position, Center.transform.position, step);
    }
     void controlPositionZ()
    {
        transform.position = Vector3.MoveTowards(transform.position, Center.transform.position, step);
    }

     void SpinRotationY()
    {
      
    }
}

