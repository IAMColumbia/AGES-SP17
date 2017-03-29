using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spawnTimer : MonoBehaviour {

    public delegate void spawnTimerCallback();
    Image timerImage;

    spawnTimerCallback callback;

    [SerializeField]
    Color fillColor, finishedColor;

	// Use this for initialization
	void Start () {
        timerImage = GetComponent<Image>();
        HideTimer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void StartTimer(float time, spawnTimerCallback c)
    {
        callback = c;

        timerImage.enabled = true;
        timerImage.color = fillColor;

        StartCoroutine(runTimer(time));
    }

    void EndTimer()
    {
        timerImage.color = finishedColor;
        callback.Invoke();
    }

    public void HideTimer()
    {
        timerImage.fillAmount = 0;
    }

    IEnumerator runTimer(float time)
    {
        float t = 0;
        while(t < time)
        {
            timerImage.fillAmount = t / time;
            t += Time.deltaTime;
            yield return 0;
        }

        timerImage.fillAmount = 1;
        EndTimer();
    }
}
