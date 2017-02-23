using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {
    [SerializeField]
    Slider progressSlider;

    const string loadingSceneName = "LoadingScreen";
    private static string sceneToLoad = "demo scene";

	// Use this for initialization
	void Start ()
    {
        progressSlider.value = 0;
        StartCoroutine(BeginLoading());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void LoadNewScene(string sceneToLoad)
    {

        LoadingScene.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene(loadingSceneName);

    }

    private IEnumerator BeginLoading()
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation async =  SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while(!async.isDone)
        {
            progressSlider.value = async.progress;

            if (async.progress > 0 && async.progress < 1)
                yield return new WaitForSeconds(1f);

            yield return null;
        }

        progressSlider.value = async.progress;

        yield return new WaitForSeconds(0.5f);

        SceneManager.UnloadScene(loadingSceneName);
    }

}
