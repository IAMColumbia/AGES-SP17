using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public int pauseForHowLong = 3;
    public GameObject Text3;
    public GameObject Text2;
    public GameObject Text1;
    public GameObject TextGo;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Pause(pauseForHowLong));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.realtimeSinceStartup > 5)
        {
            TextGo.SetActive(false);
        }
    }

    //Code here credited to an answer given by Rayan Adam @ answers.Unity3d
    //http://answers.unity3d.com/questions/346970/wait-3-seconds-then-resume-c.html
    //TimeWizard

    private IEnumerator Pause(int p)
    {
        //Start scene off with timescale set to 0
        Time.timeScale = 0f;
        //setup our timer
        float pauseEndTime = Time.realtimeSinceStartup + p;
        //add a condition for our timer to reach
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        //Fulfill the action once the timer's mark is hit
        Time.timeScale = 1;
        TextGo.SetActive(true);
    }

    //private void TimeWizard()
    //{
    //    if (Time.realtimeSinceStartup == 0)
    //    {
    //        Text3.SetActive(true);
    //    }
    //    if (Time.realtimeSinceStartup == 1)
    //    {
    //        Text3.SetActive(false);
    //        Text2.SetActive(true);
    //    }
    //    if (Time.realtimeSinceStartup == 2)
    //    {
    //        Text2.SetActive(false);
    //        Text1.SetActive(true);
    //    }
    //    if (Time.realtimeSinceStartup == 3)
    //    {
    //        Text1.SetActive(false);
    //        TextGo.SetActive(true);
    //    }

    //    if (Time.realtimeSinceStartup == 4)
    //    {
           
    //        TextGo.SetActive(false);
    //    }
    //}
}
