using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField]
    GameObject P1;

    [SerializeField]
    Vector3 P1Location;

    [SerializeField]
    GameObject P2;

    [SerializeField]
    Vector3 P2Location;

    [SerializeField]
    float cameraHeight;

    [SerializeField]
    float cameraX;

    [SerializeField]
    float cameraZ;

    [SerializeField]
    Vector3 playersMidPoint;

    [SerializeField]
    float cameraSpeed = 1f;

	// Use this for initialization
	void Start (){
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        cameraHeight = Vector3.Distance(P1.transform.position, P2.transform.position) * 1.25f;

        P1Location = P1.transform.position;
        P2Location = P2.transform.position;

        transform.position = new Vector3(playersMidPoint.x - 10f, cameraHeight, playersMidPoint.z + 10f);

        playersMidPoint = (P1Location + P2Location) / 2;

        Vector3 targetDir = playersMidPoint - transform.position;
        float cameraTurnSpeed = cameraSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, cameraTurnSpeed, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

    }
}
