using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScene : MonoBehaviour

{
    [SerializeField]
    Slider progressSlider;

    private string sceneToLoad = "demo scene";
   

	// Use this for initialization
	void Start ()
    {
        SceneManager.LoadScene(sceneToLoad);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
