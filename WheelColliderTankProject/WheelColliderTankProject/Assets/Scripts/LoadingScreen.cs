using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] GameObject loadingScreenPanel;
    [SerializeField] Slider progressSlider;

    void Awake()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(loadingScreenPanel);
        DontDestroyOnLoad(this);
    }

    public void ButtonClicked()
    {
        Debug.Log("Da buttun wuz click");
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        progressSlider.value = 0;

        while(!async.isDone)
        {
            progressSlider.value = async.progress;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        progressSlider.value = async.progress;

        loadingScreenPanel.SetActive(false);
    }
}
