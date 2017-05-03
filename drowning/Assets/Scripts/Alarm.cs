using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour {

    [SerializeField]
    GameObject AlarmLight;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TurnOnAlarm()
    {
        anim.SetTrigger("On");
        AlarmLight.SetActive(true);
    }

    public void TurnOffAlarm()
    {
        anim.SetTrigger("Off");
        AlarmLight.SetActive(false);
    }
}
