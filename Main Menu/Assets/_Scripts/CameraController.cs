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


	void Start ()
    {
        ResetCamera();
	}
	
	void Update ()
    {
        MoveCameraToGamePosition();

        RotateCameraToGamePosition();
    }

    public void ResetCamera()
    {
        startTime = Time.time;
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
        transform.rotation = Quaternion.Slerp(startCameraPoint.rotation, gamePlayCameraPoint.rotation, Time.time * cameraRotationSpeed);
    }
}
