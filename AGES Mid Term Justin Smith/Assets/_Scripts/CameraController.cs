using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform startCameraPoint;
    [SerializeField]
    Transform gamePlayCameraPoint;

    float cameraMoveSpeed = 10.0f;
    float cameraRotationSpeed = .25f;
    float startTime;
    float distanceToMoveCamera;
    float timeToWait = 3.0f;
    bool hasFinishedWaiting = false;

	void Start ()
    {
        ResetCamera();
        StartCoroutine(WaitForRoundToStart());
	}
	
	void Update ()
    {
        if (hasFinishedWaiting)
        {
            MoveCameraToGamePosition();

            RotateCameraToGamePosition();
        }
    }

    IEnumerator WaitForRoundToStart()
    {
        yield return new WaitForSeconds(timeToWait);

        hasFinishedWaiting = true;
        startTime = Time.time;
    }

    public void ResetCamera()
    {
        distanceToMoveCamera = Vector3.Distance(startCameraPoint.position, gamePlayCameraPoint.position);
        transform.position = startCameraPoint.position;
        transform.rotation = startCameraPoint.rotation;
    }

    void MoveCameraToGamePosition()
    {
        float distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
        float fractionOfDistance = distanceCovered / distanceToMoveCamera;
        transform.position = Vector3.Lerp(startCameraPoint.position, gamePlayCameraPoint.position, fractionOfDistance);
    }

    void RotateCameraToGamePosition()
    {
        transform.rotation = Quaternion.Slerp(startCameraPoint.rotation, gamePlayCameraPoint.rotation, (Time.time - startTime) * cameraRotationSpeed);
    }
}
