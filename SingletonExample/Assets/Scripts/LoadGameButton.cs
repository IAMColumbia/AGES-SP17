using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour {

    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    GameObject loadingScreenPanel;

    [SerializeField]
    Slider progressSlider;

	public void ButtonClicked()
    {
        Debug.Log("Button Clicked");
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
    }

    void Awake()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator LoadNewScene()
    {

        progressSlider.value = 0;

        yield return new WaitForSeconds(0.5f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            
            progressSlider.value = async.progress;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        progressSlider.value = async.progress;

        Destroy(gameObject);
    }
}
