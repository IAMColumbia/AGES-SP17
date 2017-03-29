using UnityEngine;
using System.Collections;
using System;

public class LoadNextScene : MonoBehaviour, Observer {

    [SerializeField]
    Wave lastWave;

    [SerializeField]
    float waitTime;

    [SerializeField]
    string nextScene;

    public void Notify(Subject sender, Event e)
    {
        if (e == Event.waveEnded)
        {
            endGame();
        }
    }

    // Use this for initialization
    void Start () {
        lastWave.Subscribe(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void endGame()
    {
        StartCoroutine(loadNextScene());
    }

    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(waitTime);

        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
