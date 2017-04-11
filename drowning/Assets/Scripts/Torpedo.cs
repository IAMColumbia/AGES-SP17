using UnityEngine;
using System.Collections;

public class Torpedo : MonoBehaviour {

    [SerializeField]
    TorpedoInfoPanel infoPanelPrefab;

    TorpedoInfoPanel torpedoInfo = null;

    public Transform torpedoInfoList;

    public float FuseTime = 20, Speed = 5, Heading = 0;

    float remainingFuse;

    Vector3 unitMovement;

    bool fired = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (fired)
        {
            transform.localPosition += unitMovement * Speed * Time.fixedDeltaTime;

            if(torpedoInfo != null)
            {
                torpedoInfo.updateCoordinateText(transform.localPosition);
            }

            remainingFuse -= Time.deltaTime;

            if(remainingFuse <= 0)
            {
                torpedoInfo.updateStatus("MISSED");
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

    public void HitEnemy()
    {
        torpedoInfo.updateStatus("KILL CONFIRMED");
        Die();
    }

    void Die()
    {
        torpedoInfo.Remove(3);
        Destroy(this.gameObject);
    }
}
