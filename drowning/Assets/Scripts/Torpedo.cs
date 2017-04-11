using UnityEngine;
using System.Collections;

public class Torpedo : MonoBehaviour {

    [SerializeField]
    TorpedoInfoPanel infoPanelPrefab;

    TorpedoInfoPanel torpedoInfo = null;

    Transform torpedoInfoList;

    public float FuseTime = 6, Speed = 1, Heading = 0;

    float remainingFuse;

    Vector3 unitMovement;

    bool fired = false;

	// Use this for initialization
	void Start () {
        TorpedoUI t = FindObjectOfType<TorpedoUI>();
        torpedoInfoList = t.torpedoInfoList;
	}
	
	// Update is called once per frame
	void Update () {
        if (fired)
        {
            transform.localPosition += unitMovement * Speed;

            if(torpedoInfo != null)
            {
                torpedoInfo.updateCoordinateText(transform.localPosition);
            }

            remainingFuse -= Time.deltaTime;

            if(remainingFuse <= 0)
            {
                Die();
            }
        }
	}

    public void Fire()
    {
        remainingFuse = FuseTime;
        float theta = (90 - Heading) * Mathf.Deg2Rad; //correct heading info from 0 straight forward and clockwise to 0 at right and anticlockwise

        unitMovement = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));

        torpedoInfo = Instantiate<TorpedoInfoPanel>(infoPanelPrefab);
        torpedoInfo.transform.SetParent(torpedoInfoList, false);

        torpedoInfo.updateStatus("TARGET LOCKED");

        fired = true;
    }

    void Die()
    {
        torpedoInfo.updateStatus("KILL CONFIRMED");
        Destroy(torpedoInfo.gameObject, 3);
        Destroy(this);
    }
}
