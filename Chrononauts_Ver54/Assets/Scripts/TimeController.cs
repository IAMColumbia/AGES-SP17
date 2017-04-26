using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    [SerializeField]
    public Text timerUI;

    public float globalTimer = 30.0f;

	// Use this for initialization
	void Start ()
    {
        timerUI.text = globalTimer.ToString();
        InvokeRepeating("TimeWizard", 0, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void FixedUpdate()
    {

    }

    void TimeWizard()
    {
        Debug.Log("FUG :DDD");
        globalTimer--;
        timerUI.text = globalTimer.ToString();
    }

    void Invoke()
    {
       
        
    }
}
