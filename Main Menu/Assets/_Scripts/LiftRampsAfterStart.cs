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
    float secondsToWaitBeforeLiftingRamp = 3.5f;
    
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
        if (canLiftRamp)
            LiftRampAboveRing();
    }

    void LiftRampAboveRing()
    {
        float distanceCovered = (Time.time - startTime) * liftSpeed;
        float fractionOfDistance = distanceCovered / distanceToMoveRamp;
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfDistance);
    }

    IEnumerator WaitThenGiveOkToLiftRamp()
    {
        yield return new WaitForSeconds(secondsToWaitBeforeLiftingRamp);
        startTime = Time.time;
        canLiftRamp = true;
    }
}
