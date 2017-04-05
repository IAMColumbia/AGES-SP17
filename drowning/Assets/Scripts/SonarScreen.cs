using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SonarScreen : MonoBehaviour {

    const float screenRadius = 256;
    float maxRange = 100;

    [SerializeField]
    Transform pingPanel;

    [SerializeField]
    Image pingPrefab;

    [SerializeField]
    float pingFadeRate, sweepSpeed;

    [SerializeField]
    Sweep sweep;

    List<SonarPing> pingsOnScreen = new List<SonarPing>();

    bool spawningPings = true;

	// Use this for initialization
	void Start () {
        SpawnRandomPing();
        SpawnRandomPing();
        SpawnRandomPing();
        SpawnRandomPing();
        SpawnRandomPing();
        SpawnRandomPing();
        SpawnRandomPing();
        SpawnRandomPing();
        SpawnRandomPing();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = pingsOnScreen.Count -1; i >=0; i--)
        {
            if(Mathf.Abs(sweep.t - pingsOnScreen[i].theta) < .1)
            {
                pingsOnScreen[i].SetActive();
            }

            if (pingsOnScreen[i].Active)
            {
                pingsOnScreen[i].Decay(Time.deltaTime * pingFadeRate);
            }
        }
	}

    IEnumerator spawnPingsRandomly(float rate)
    {
        while (spawningPings)
        {
            yield return new WaitForSeconds(rate);

            SpawnRandomPing();
        }
    }

    void SpawnRandomPing()
    {
        float t = Random.Range(0, 6.28f);
        float r = Random.Range(0, maxRange);

        SpawnPingAt(t, r);
    }

    void SpawnPingAt(float theta, float range)
    {
        float rangeOnScreen = Mathf.Clamp(range, 0, maxRange) / maxRange * screenRadius; //convert range units to screen units

        Vector2 targetLocation = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * rangeOnScreen;

        Image newPing = Instantiate<Image>(pingPrefab);
        newPing.transform.SetParent(pingPanel, false);

        newPing.rectTransform.anchorMin = Vector2.one / 2;
        newPing.rectTransform.anchorMax = Vector2.one / 2;

        newPing.rectTransform.anchoredPosition = targetLocation;

        SonarPing p = new SonarPing(theta, range, newPing);

        pingsOnScreen.Add(p);
    }
}

public class SonarPing
{
    Image pingImage;

    public float theta, range;

    bool active = false;

    public bool Active
    {
        get { return active; }
    }

    public SonarPing(float _theta, float _range, Image _image)
    {
        theta = _theta;
        range = _range;
        pingImage = _image;

        Decay(9999); // ping starts invisible
    }

    public void Decay(float amount)
    {
        Color c = pingImage.color;
        c.a -= amount;
        pingImage.color = c;

        if (c.a <= 0)
        {
            active = false;
        }
    }

    public void SetActive()
    {
        Color c = pingImage.color;
        c.a = 1;
        pingImage.color = c;

        active = true;
    }
}
