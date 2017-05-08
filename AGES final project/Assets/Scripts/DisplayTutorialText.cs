using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayTutorialText : MonoBehaviour
{
    [SerializeField]
    Text tutorialText;
    [SerializeField]
    string text;
	// Use this for initialization
	void Start ()
    {
        tutorialText.gameObject.SetActive(false);
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            tutorialText.gameObject.SetActive(true);
            tutorialText.text = text;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        tutorialText.gameObject.SetActive(false);
    }
}
