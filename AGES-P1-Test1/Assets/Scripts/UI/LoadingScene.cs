using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    Slider progressSlider;

    [SerializeField]
    static string sceneToLoad = "testscene";

    const string loadingSceneName = "LoadingScene";

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(BeginLoading());

        progressSlider.value = 0f;
	}
	
    public static void LoadNewScene(string sceneToLoad)
    {
        LoadingScene.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene(loadingSceneName);
    }

    private IEnumerator BeginLoading()
    {
        

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while(!async.isDone)
        {
            progressSlider.value = async.progress;

            yield return null;

            
        }

        yield return new WaitForSeconds(1f);
        SceneManager.UnloadScene(loadingSceneName);
    }


}
