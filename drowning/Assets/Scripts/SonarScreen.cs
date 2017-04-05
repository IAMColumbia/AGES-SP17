using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SonarScreen : MonoBehaviour {

    const float screenRadius = 45;
    float maxRange = 100;

    [SerializeField]
    Transform pingPanel;

    [SerializeField]
    Image pingPrefab;

    [SerializeField]
    float pingFadeRate;

    List<Image> pingsOnScreen = new List<Image>();

    bool spawningPings = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(spawnPingsRandomly(1));
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = pingsOnScreen.Count -1; i >=0; i--)
        {
            Image sonarPing = pingsOnScreen[i];

            Color c = sonarPing.color;
            c.a -= Time.deltaTime * pingFadeRate;

            sonarPing.color = c;

            if (c.a <= 0)
            {
                pingsOnScreen.RemoveAt(i);
                Destroy(sonarPing.gameObject);
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

        Vector2 targetLocation = new Vector2(Mathf.Sin(theta), Mathf.Cos(theta)) * rangeOnScreen;

        Image newPing = Instantiate<Image>(pingPrefab);
        newPing.transform.SetParent(pingPanel, false);

        newPing.rectTransform.anchorMin = Vector2.one / 2;
        newPing.rectTransform.anchorMax = Vector2.one / 2;

        newPing.rectTransform.anchoredPosition = targetLocation;

        pingsOnScreen.Add(newPing);
    }
}

public class SonarPing
{

}
