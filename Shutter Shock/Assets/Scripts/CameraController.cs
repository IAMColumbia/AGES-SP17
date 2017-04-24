using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public CanvasGroup myCG;

    private Player player;
    private bool flash = false;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void FixedUpdate ()
    {
        if (flash)
        {
            Debug.Log("This works");
            myCG.alpha = myCG.alpha - Time.deltaTime * 0.3f;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(StartCountdown());
        }
    }

    private IEnumerator StartCountdown()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(.15f);
        }

        TakePicture();
    }

    private void TakePicture()
    {
        if (player.IsPlayerScrewed())
        {
            flash = true;
            myCG.alpha = 1;
        }

        else
        {

        }
    }
}
