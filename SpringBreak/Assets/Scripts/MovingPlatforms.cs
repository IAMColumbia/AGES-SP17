using UnityEngine;
using System.Collections;
using System;

public class MovingPlatforms : MonoBehaviour {

    // Use this for initialization
    float speed = 25;
    // Update is called once per frame
    [SerializeField]
    float velocidadMax;
    [SerializeField]
    float xMax;
    [SerializeField]
    float zMax;
    [SerializeField]
    float xMin;
    [SerializeField]
    float zMin;

    private float x;
    private float z;
    private float tiempo;
    private float angulo;

    // Use this for initialization
    void Start()
    {


        x = UnityEngine.Random.Range(-velocidadMax, velocidadMax);
        z = UnityEngine.Random.Range(-velocidadMax, velocidadMax);
        angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
        transform.localRotation = Quaternion.Euler(0, angulo, 0);
    }

    // Update is called once per frame
    void Update()
    {

        tiempo += Time.deltaTime;

        if (transform.localPosition.x > xMax)
        {
            x = UnityEngine.Random.Range(-velocidadMax, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }
        if (transform.localPosition.x < xMin)
        {
            x = UnityEngine.Random.Range(0.0f, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }
        if (transform.localPosition.z > zMax)
        {
            z = UnityEngine.Random.Range(-velocidadMax, 0.0f);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }
        if (transform.localPosition.z < zMin)
        {
            z = UnityEngine.Random.Range(0.0f, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }


        if (tiempo > 1.0f)
        {
            x = UnityEngine.Random.Range(-velocidadMax, velocidadMax);
            z = UnityEngine.Random.Range(-velocidadMax, velocidadMax);
            angulo = Mathf.Atan2(x, z) * (180 / 3.141592f) + 90;
            transform.localRotation = Quaternion.Euler(0, angulo, 0);
            tiempo = 0.0f;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z + z);
    }
  
}

