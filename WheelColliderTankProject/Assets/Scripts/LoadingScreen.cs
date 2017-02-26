using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen: MonoBehaviour {

    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private GameObject loadingScreenPanel;

    [SerializeField]
    private Slider progressSlider;

    private void Awake()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(loadingScreenPanel);
    }

    public void ButtonClicked()
    {
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        progressSlider.value = 0;
        
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while(!async.isDone)
        {
            progressSlider.value = async.progress;
            yield return new WaitForSeconds(.2f);
        }

        yield return new WaitForSeconds(2);

        progressSlider.value = 1;

        loadingScreenPanel.SetActive(false);

    }

}
