using UnityEngine;
using System.Collections;
using System;

public class CodecLoadScene : MonoBehaviour {


    [SerializeField]
    string SceneToLoad;

    AudioSource audiosource;

    float waitToLoad;
	
	// Update is called once per frame
	void Start ()
    {
        audiosource = GetComponent<AudioSource>();
        waitToLoad = audiosource.clip.length;

        StartCoroutine(ActivateLoad());
    }

    IEnumerator ActivateLoad()
    {
        yield return new WaitForSeconds(2f);
        audiosource.Play();

        yield return new WaitForSeconds(waitToLoad);
        LoadingScene.LoadNewScene(SceneToLoad);
    }
}
