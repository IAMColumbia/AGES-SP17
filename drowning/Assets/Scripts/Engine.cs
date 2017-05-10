using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {


    Animator anim;

    bool wonGame = false;

    PowerUI power;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        power = FindObjectOfType<PowerUI>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void winGame()
    {
        wonGame = true;
        anim.SetTrigger("win");
    }

    public void endOfGameWinningAnimation()
    {
        GameSceneManager.instance.GoToMenu();
    }
}
