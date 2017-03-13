using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    GameObject loadingScreenPanel;
    [SerializeField]
    Slider progressSlider;

    // Use this for initialization
    void Awake()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(loadingScreenPanel);
    }

    public void ButtonClicked(string sceneName)
    {
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene(sceneName));
    }

    private IEnumerator LoadNewScene(string sceneToLoad)
    {
        progressSlider.value = 0;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!async.isDone)
        {
            progressSlider.value = async.progress;
            yield return null;
        }

        progressSlider.value = 1;
        yield return new WaitForSeconds(0.5f);
        loadingScreenPanel.SetActive(false);
    }
}
