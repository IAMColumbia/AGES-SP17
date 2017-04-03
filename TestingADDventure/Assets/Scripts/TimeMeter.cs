using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeMeter : MonoBehaviour
{
    [SerializeField]
    float startTime;

    [HideInInspector]
    public float timeLeft { get; set; }
    [HideInInspector]
    public bool startHasBeenPressed { get; set; }

    void Start()
    {
        GetComponent<Slider>().maxValue = startTime;
        startHasBeenPressed = false;
        timeLeft = startTime;
    }

    void Update()
    {
        if (startHasBeenPressed)
        {
            TimerStart();
        }
    }

    void TimerStart()
    {
        timeLeft -= Time.deltaTime;
        GetComponent<Slider>().value = timeLeft;

        if (timeLeft < 0)
        {
            timeLeft = 0;
        }
    }
}
