using UnityEngine;
using System.Collections;

public class TorpedoLauncher : MonoBehaviour {

    [SerializeField]
    Torpedo torpedoPrefab;

    float speed = 10;
    float fuseTime = 6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LaunchTorpedo(float heading)
    {
        Torpedo newTorpedo = Instantiate<Torpedo>(torpedoPrefab);
        newTorpedo.transform.SetParent(transform, false);

        newTorpedo.Speed = speed;
        newTorpedo.FuseTime = fuseTime;
        newTorpedo.Heading = heading;

        newTorpedo.Fire();
    }
}
