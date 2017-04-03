using UnityEngine;
using System.Collections;
using System;

public class CameraScript : MonoBehaviour {
    [SerializeField]
    float DampTime = 0.2f;
    [SerializeField]
    float EdgeBuffer = 4f;
    [SerializeField]
    float minSize = 6.5f;

    public Transform[] Targets;

    Camera myCamera;
    float zoomSpeed;
    Vector3 moveVelocity;
    Vector3 desiredPosition;

    void Awake()
    {
        myCamera = GetComponentInChildren<Camera>();
    }

    void FixedUpdate()
    {
        MoveCamera();
        Zoom();
    }
	void FindAveragePosition()
    {
        Vector3 averagePosition = new Vector3();
        int numberOfTargets = 0;

        for (int i = 0; i < Targets.Length; i++)
        {
            if(Targets[i].gameObject.activeSelf)
            {
                continue;
            }
            averagePosition += Targets[i].position;
            numberOfTargets++;
        }

        if(numberOfTargets>0)
        {
            averagePosition /= numberOfTargets;
        }

        averagePosition.y = transform.position.y;

        desiredPosition = averagePosition;
    }

    void MoveCamera()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, DampTime);
    }

    void Zoom()
    {
        float requiredSize = FindRequiredSize();
        myCamera.orthographicSize = Mathf.SmoothDamp(myCamera.orthographicSize, requiredSize, ref zoomSpeed, DampTime);
    }

    float FindRequiredSize()
    {
        Vector3 desiredLocalPosition = transform.InverseTransformPoint(desiredPosition);

        float size = 0f;

        for (int i = 0; i < Targets.Length; i++)
        {
            if(!Targets[i].gameObject.activeSelf)
            {
                continue;
            }
            Vector3 targetLocalPosition = transform.InverseTransformDirection(Targets[i].position);

            Vector3 desiredPositionToTarget = targetLocalPosition - desiredLocalPosition;

            size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPositionToTarget.x) / myCamera.aspect);
        }
        size += EdgeBuffer;
        size = Mathf.Max(size, minSize);
        return size;
    }

    public void SeStartPositionAndSize()
    {
        FindAveragePosition();
        transform.position = desiredPosition;
        myCamera.orthographicSize = FindRequiredSize();
    }
}
