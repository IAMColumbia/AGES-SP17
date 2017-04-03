using UnityEngine;
using System.Collections;

public class MoveToQuestion : MonoBehaviour
{
    [SerializeField]
    Transform question;
    [SerializeField]
    float speed = 5;

    Vector3 currentPositionY;
    //Vector3 velocity;
    float startTime;
    float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, question.position);
    }

    void Update()
    {
        currentPositionY = new Vector3(0, transform.position.y, -10);
        float distCovered = (Time.time - startTime) * speed;
        //float fracJourney = distCovered / journeyLength;
        float fracJourney = speed / journeyLength;

        //Debug.Log(fracJourney);
        //transform.LookAt(question.position);
        //velocity = Vector3.up;
        //transform.position = Vector3.Lerp(currentPositionY, new Vector3(0, question.position.y, -10), fracJourney);
        //transform.Translate(Vector3.down);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}
