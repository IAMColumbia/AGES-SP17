using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuImageManager : MonoBehaviour
{
    [SerializeField]
    Image[] images;
    [SerializeField]
    Button playButton;
    [SerializeField]
    Button creditsButton;
    [SerializeField]
    Button exitButton;

    public void MoveToPlayButton()
    {
        for (int i = 0; i < images.Length; i++)
        {
            Vector3 position = images[i].transform.position;
            position[1] = playButton.transform.position.y;
            images[i].transform.position = position;
        }
    }

    public void MoveToCreditsButton()
    {
        for (int i = 0; i < images.Length; i++)
        {
            Vector3 position = images[i].transform.position;
            position[1] = creditsButton.transform.position.y;
            images[i].transform.position = position;
        }
    }

    public void MoveToExitButton()
    {
        for (int i = 0; i < images.Length; i++)
        {
            Vector3 position = images[i].transform.position;
            position[1] = exitButton.transform.position.y;
            images[i].transform.position = position;
        }
    }
}
