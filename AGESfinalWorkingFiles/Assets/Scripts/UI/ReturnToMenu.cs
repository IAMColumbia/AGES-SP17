using UnityEngine;
using System.Collections;

public class ReturnToMenu : MonoBehaviour
{
    [SerializeField]
    string MainMenu;

    public void ReturnButtonClicked()
    {
        LoadingScene.LoadNewScene(MainMenu);
    }

}
