using UnityEngine;
using System.Collections;

public class ClickLinkScript : MonoBehaviour {

    // Use this for initialization
    void Start() {


    }
    public void ShowRiseofSpiritCredit()
    {
        Application.OpenURL("https://opengameart.org/content/rise-of-spirit");       
    }
    public void ShowTanksTutorialCredit()
    {
        Application.OpenURL("https://unity3d.com/learn/tutorials/projects/tanks-tutorial");
    }
    public void ShowOpenGameArtCredit()
    {
        Application.OpenURL("https://opengameart.org/");
    }
}
	
	// Update is called once per frame
	
