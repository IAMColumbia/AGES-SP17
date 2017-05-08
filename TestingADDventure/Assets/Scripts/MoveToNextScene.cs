using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToNextScene : MonoBehaviour
{
    [SerializeField]
    string nextScene;
    [SerializeField]
    Image fadeOutImage;
    [SerializeField]
    AudioSource musicLoop;

    public void LoadNextScene()
    {
        StartCoroutine(FadeOutToNextScene());
    }

    IEnumerator FadeOutToNextScene()
    {
        fadeOutImage.gameObject.SetActive(true);

        for (int i = 0; i < 100; i++)
        {
            fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, fadeOutImage.color.a + 0.01f);
            musicLoop.volume -= 0.01f;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        SceneManager.LoadScene(nextScene);
    }
}
