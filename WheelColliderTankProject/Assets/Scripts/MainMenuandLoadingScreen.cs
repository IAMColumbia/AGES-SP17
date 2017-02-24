using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuandLoadingScreen : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;
    [SerializeField]
    GameObject loadingScreenPanel;
    [SerializeField]
    Slider progressSlider;

	// Use this for initialization
	void Awake ()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
	
	}

    public void ButtonClicked()
    {
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        //yield return new WaitForSeconds(0.5f);

        progressSlider.value = 0;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            progressSlider.value = async.progress;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        progressSlider.value = async.progress;
        loadingScreenPanel.SetActive(false);
    }

}
