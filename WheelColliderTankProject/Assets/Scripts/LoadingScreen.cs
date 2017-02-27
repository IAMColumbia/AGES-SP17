using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;
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

    public void ButtonClicked()
    {
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
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
