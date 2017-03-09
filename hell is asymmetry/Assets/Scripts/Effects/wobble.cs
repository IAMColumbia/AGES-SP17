using UnityEngine;
using System.Collections;

public class wobble : MonoBehaviour {

    [SerializeField]
    float minRotation, maxRotation, rate;

    float range;

    // Use this for initialization
    void Start()
    {
        range = maxRotation - minRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = minRotation + (range / 2) * (Mathf.Cos(2 * Mathf.PI * rate * Time.time) + 1f);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
