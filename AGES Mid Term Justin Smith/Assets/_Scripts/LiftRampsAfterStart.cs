using UnityEngine;
using System.Collections;

public class LiftRampsAfterStart : MonoBehaviour
{
    Transform startPoint;
    Transform endPoint;

    bool canLiftRamp;
    float liftSpeed = 10.0f;
    float startTime;
    float distanceToMoveRamp;
    float secondsToWaitBeforeLiftingRamp = 6.5f;
    //float timeToWait = 3.0f;
    //bool hasFinishedWaiting = false;
    
	void Start ()
    {
        startPoint = GameObject.Find("RampSpawnPoint").GetComponent<Transform>();
        endPoint = GameObject.Find("RampEndPoint").GetComponent<Transform>();
        canLiftRamp = false;
        distanceToMoveRamp = Vector3.Distance(startPoint.position, endPoint.position);
        StartCoroutine(WaitThenGiveOkToLiftRamp());
        
    }

    void Update()
    {
        //if (hasFinishedWaiting && !canLiftRamp)
        //    StartCoroutine(WaitThenGiveOkToLiftRamp());

        if (canLiftRamp)
            LiftRampAboveRing();
    }

    void LiftRampAboveRing()
    {
        float distanceCovered = (Time.time - startTime) * liftSpeed;
        float fractionOfDistance = distanceCovered / distanceToMoveRamp;
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfDistance);
    }

    //IEnumerator WaitForRoundToStart()
    //{
    //    yield return new WaitForSeconds(timeToWait);

    //    hasFinishedWaiting = true;
    //}

    IEnumerator WaitThenGiveOkToLiftRamp()
    {
        yield return new WaitForSeconds(secondsToWaitBeforeLiftingRamp);
        startTime = Time.time;
        canLiftRamp = true;
    }
}
