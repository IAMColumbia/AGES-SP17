using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

    [SerializeField]

    Slider progressSlider;

    private string sceneToLoad;
	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(sceneToLoad);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
