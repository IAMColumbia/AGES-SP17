using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    Slider progressSlider;

    const string loadingSceneName = "LoadingScene";
    private static string sceneToLoad = "demo scene";

	// Use this for initialization
	void Start ()
    {
        progressSlider.value = 0;
        StartCoroutine(BeginLoading());
	}

    public static void loadNewScene(string sceneToLoad)
    {
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

            if(async.progress > 0 && async.progress > 1)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }

        progressSlider.value = async.progress;

        yield return new WaitForSeconds(0.5f);

        SceneManager.UnloadScene("LoadingScene");
    }
}
