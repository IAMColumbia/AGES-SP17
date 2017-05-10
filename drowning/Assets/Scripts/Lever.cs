using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

    [SerializeField]
    PowerUI power;

    [SerializeField]
    Engine engine;

    Animator anim;

    bool leverPulled = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    private void OnMouseDown()
    {
        if (power.IsAtFullPower && !leverPulled)
        {
            leverPulled = true;
            anim.SetTrigger("Pull");
            engine.winGame();
        }
        else if(!power.IsAtFullPower)
        {
            anim.SetTrigger("Stuck");
        }
    }
}
