using UnityEngine;
using System.Collections;

public class LiftRampsAfterStart : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;

    Transform startPoint;
    Transform endPoint;

    bool canLiftRamp;
    bool isFirstRound = true;
    float liftSpeed = 10.0f;
    float startTime;
    float distanceToMoveRamp;
    WaitForSeconds secondsToWaitBeforeLiftingRamp = new WaitForSeconds(6.5f);
    WaitForSeconds secondsToWaitAtRoundStart = new WaitForSeconds(3.0f);
    Quaternion spawnRotation;
    //float timeToWait = 3.0f;
    //bool hasFinishedWaiting = false;
    
	void Start ()
    {
        startPoint = GameObject.Find("RampSpawnPoint").GetComponent<Transform>();
        endPoint = GameObject.Find("RampEndPoint").GetComponent<Transform>();
        canLiftRamp = false;
        distanceToMoveRamp = Vector3.Distance(startPoint.position, endPoint.position);
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
    
    public IEnumerator WaitThenGiveOkToLiftRamp()
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        if (isFirstRound)
            yield return secondsToWaitBeforeLiftingRamp;
        else
            yield return secondsToWaitAtRoundStart;

        startTime = Time.time;
        canLiftRamp = true;
    }

    public void Reset()
    {
        canLiftRamp = false;
        StartCoroutine(WaitThenGiveOkToLiftRamp());
    }
}
