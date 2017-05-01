using UnityEngine;
using System.Collections;

public class MainMenuQuitTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject YButtonSprite;

    private void OnTriggerStay2D(Collider2D collision)
    {
        YButtonSprite.SetActive(true);

        if(Input.GetButtonDown("MainMenuButton"))
            Application.Quit();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        YButtonSprite.SetActive(false);
    }
}
