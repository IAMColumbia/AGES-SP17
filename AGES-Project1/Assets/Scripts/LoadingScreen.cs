using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {
    [SerializeField]
    Slider progressSlider;

    const string loaddingSceneName = "LoadingScreen";
    private static string sceneToLoad = "Level";

	// Use this for initialization
	void Start ()
    {
        progressSlider.value = 0;
        StartCoroutine(BeginLoading());
	}
	
	// Update is called once per frame
	public static void LoadNewScene(string sceneToLoad)
    {
        LoadingScreen.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene(loaddingSceneName);
    }

    private IEnumerator BeginLoading()
    {

        yield return new WaitForSeconds(0.5f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);


        while(!async.isDone)
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
        
        SceneManager.UnloadScene(loaddingSceneName);
        
    }
}
