using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    Slider progressSlider;

    private const string loadingSceneName = "loadingScene";
    private static string sceneToLoad = "demo scene"; 

	void Start () {
        progressSlider.value = 0;
        StartCoroutine(BeginLoading());
	}

    public static void LoadNewScene(string sceneToLoad)
    {
        //Static scene to load vs regular scene to load
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
            if(async.progress > 0 && async.progress < 1)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
        progressSlider.value = async.progress;
        yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadScene(loadingSceneName);
       
    }

    // Update is called once per frame
}
