using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    GameObject fadeObject;

    public void Fade(bool fadeout)
    {
        StartCoroutine(FadeImage(fadeout));
    }

    IEnumerator FadeImage(bool fadeOut)
    {
        if (fadeObject.GetComponent<Text>())
        {
            if (fadeOut)
            {
                for (float i = 1; i >= 0; i -= 2 * Time.deltaTime)
                {
                    fadeObject.GetComponent<Text>().color = new Color(1, 1, 1, i);
                    yield return null;
                }

                for (float i = 0; i <= 1; i += 2 * Time.deltaTime)
                {
                    fadeObject.GetComponent<Text>().color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            else if (!fadeOut)
            {
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    fadeObject.GetComponent<Text>().color = new Color(1, 1, 1, i);
                    yield return null;
                }

                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    fadeObject.GetComponent<Text>().color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }
        else if (fadeObject.GetComponent<Image>())
        {
            if (fadeOut)
            {
                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    fadeObject.GetComponent<Image>().color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            else if (!fadeOut)
            {
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    fadeObject.GetComponent<Image>().color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }        
    }
}
