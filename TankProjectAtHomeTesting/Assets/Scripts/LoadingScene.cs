using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    Slider progressSlider;

    CanvasGroup canvasGroup;

	// Use this for initialization
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        progressSlider.value = 0;
    }

    private void Start ()
    {
        StartCoroutine(LoadNewScene());
    }
	
    
    private IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(0.2f);

        AsyncOperation async = SceneManager.LoadSceneAsync(LoadingSceneManager.SceneToLoad, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            progressSlider.value = async.progress;
            yield return new WaitForSeconds(0.2f);            
        }

        progressSlider.value = 1;

        yield return new WaitForSeconds(0.2f);

        SceneManager.UnloadScene(LoadingSceneManager.loadingSceneName);

    }
}
