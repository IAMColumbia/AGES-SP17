using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;
    [SerializeField]
    GameObject loadingScreenPanel;
    [SerializeField]
    Slider progressSlider;

    void Awake()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ButtonClicked()
    {
        Debug.Log("clicked");
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        progressSlider.value = 0;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (async.isDone)
        {
            progressSlider.value = async.progress;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        progressSlider.value = 1;
        loadingScreenPanel.SetActive(false);
    }
}
