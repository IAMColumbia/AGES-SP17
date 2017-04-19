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
        if (Input.GetKeyDown(KeyCode.Space) && !wonGame && power.IsAtFullPower)
        {
            winGame();
        }
	}

    void winGame()
    {
        wonGame = true;
        anim.SetTrigger("win");
    }

    public void endOfGameWinningAnimation()
    {
        GameSceneManager.instance.GoToMenu();
    }
}
