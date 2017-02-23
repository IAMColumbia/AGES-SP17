using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    Slider progressSlider;

    const string loadingSceneName = "loading scene";
    private static string sceneToLoad = "demo scene";
	// Use this for initialization
	void Start ()
    {
        progressSlider.value = 0;
        StartCoroutine(BeginLoading());
	}

    public static void LoadNewScene(string sceneToLoad)
    {
        //SceneManager.LoadScene(sceneToLoad);
        LoadingScene.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene(loadingSceneName);
    }

    private IEnumerator BeginLoading()
    {
        yield return new WaitForSeconds(0.5f);

       AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            progressSlider.value = async.progress;
            yield return null;
        }

        progressSlider.value = async.progress;

        SceneManager.UnloadScene(loadingSceneName);
    }
}
