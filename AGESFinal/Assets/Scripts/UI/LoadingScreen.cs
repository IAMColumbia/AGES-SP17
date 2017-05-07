using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField]
    private static string sceneToLoad = "MainGame4Player";

    [SerializeField]
    private Slider progressSlider;

    const string loadingSceneName = "LoadingScene";

    private void Start()
    {
        progressSlider.value = 0;
        StartCoroutine(BeginLoading());
    }

    public static void LoadNewScene(string sceneToLoad)
    {
        LoadingScreen.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene(loadingSceneName);
    }
    private IEnumerator BeginLoading()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            progressSlider.value = async.progress;

            yield return null;
        }
        
        progressSlider.value = async.progress;
        
        SceneManager.UnloadScene(loadingSceneName);



    }

}
