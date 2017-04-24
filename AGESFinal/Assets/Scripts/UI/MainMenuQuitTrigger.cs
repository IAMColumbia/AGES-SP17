using UnityEngine;
using System.Collections;

public class MainMenuQuitTrigger : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetButtonDown("MainMenuButton"))
            Application.Quit();
    }
}
