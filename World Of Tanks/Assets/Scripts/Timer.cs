using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{

    [SerializeField] Text timerText;
    [SerializeField] Text recText;
    public GameObject gameOverScreen;

    string minutes;
    string seconds;
    [SerializeField] float remainingTime = 300f;
    bool gamePaused;
    AudioListener audioListener;

    void Start()
    {
        StartCoroutine(FlashLabel());
    }

    void Update()
    {
        HandleTimer();
    }

    void HandleTimer()
    {
        if (gamePaused == false)
        {
            minutes = Mathf.Floor(remainingTime / 60).ToString("00");
            seconds = (remainingTime % 60).ToString("00");

            remainingTime -= Time.deltaTime;

            timerText.text = "00:" + minutes + ":" + seconds;

            if (remainingTime < 0)
            {
                StopTimer();
                audioListener.enabled = false;
                gameOverScreen.SetActive(true);
            }
        }
    }

    void StartTimer()
    {
        gamePaused = false;
    }

    void StopTimer()
    {
        gamePaused = true;
    }

    IEnumerator FlashLabel()
    {

        while (true)
        {
            recText.enabled = true;
            yield return new WaitForSeconds(.5f);
            recText.enabled = false;
            yield return new WaitForSeconds(.5f);
        }
    }
}
