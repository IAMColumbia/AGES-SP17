using UnityEngine;
using System.Collections;
using System;

public class CameraPivot : MonoBehaviour {

    // Use this for initialization

   [SerializeField]
    GameObject cameraPivotCenter;
    [SerializeField]
    GameObject[] players; 

    [SerializeField]
    float rotationDamping;

    Vector3 cameraFollowPlayers;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        CameraRepositioning();
        

        //This rotation focuses camera at this specific angle
        transform.rotation = Quaternion.AngleAxis(-30, Vector3.left);

    }

    private void CameraRepositioning()
    {
        if(players[0] != null)
        {
            FindAveragePosition();
            for (int i = 0; i < players.Length; i++)
            {
                foreach (GameObject player in players)
                {

                }
            }
        }
        Quaternion rotation = Quaternion.LookRotation(cameraPivotCenter.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;
        for (int i = 0; i < players.Length; i++)
      {
           if (!players[i].gameObject.activeSelf)
               continue;

                    averagePos += players[i].transform.position;
                    numTargets++;
      }
        if (numTargets > 0)
        
               averagePos /= numTargets;
               averagePos.y = transform.position.y;
               cameraFollowPlayers = averagePos;
            }

}
