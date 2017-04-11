using UnityEngine;
using System.Collections;

public class TorpedoLauncher : MonoBehaviour {

    [SerializeField]
    Torpedo torpedoPrefab;

    Transform torpedoInfoList;

    float speed = 5;
    float fuseTime = 20;

	// Use this for initialization
	void Start () {
        torpedoInfoList = GameObject.FindObjectOfType<TorpedoUI>().torpedoInfoList;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LaunchTorpedo(float heading)
    {
        Torpedo newTorpedo = Instantiate<Torpedo>(torpedoPrefab);
        newTorpedo.transform.SetParent(transform, false);
        newTorpedo.torpedoInfoList = torpedoInfoList;

        newTorpedo.Speed = speed;
        newTorpedo.FuseTime = fuseTime;
        newTorpedo.Heading = heading;

        newTorpedo.Fire();
    }
}
