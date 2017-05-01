using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    Transform[] points;
    [SerializeField]
    int pointSelection;

    private Transform currentPoint;

    // Use this for initialization
    void Start ()
    {
        currentPoint = points[pointSelection];
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleMoving();
    }

    private void HandleMoving()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            pointSelection++;

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }
    }
}
