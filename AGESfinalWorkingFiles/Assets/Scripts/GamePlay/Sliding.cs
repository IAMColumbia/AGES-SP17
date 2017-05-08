using UnityEngine;
using System.Collections;

public class Sliding : MonoBehaviour
{
    #region
    [SerializeField]
    GameObject startingPoint;

    [SerializeField]
    GameObject endingPoint;

    [SerializeField]
    float speed;

    [SerializeField]
    float returnSpeed;

    [SerializeField]
    float waitToReturn;

    [SerializeField]
    bool isMovingForward;

    [SerializeField]
    Vector3 originalPos;
    #endregion

    // Use this for initialization
    void Start ()
    {
        isMovingForward = true;
        originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isMovingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, endingPoint.transform.position, speed*Time.deltaTime);

            if (transform.position == endingPoint.transform.position)
            {
                isMovingForward = false;
            }
        }

        if (!isMovingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPoint.transform.position, speed * Time.deltaTime);

            if (transform.position == startingPoint.transform.position)
            {
                isMovingForward = true;
            }
        } 
	}
}
