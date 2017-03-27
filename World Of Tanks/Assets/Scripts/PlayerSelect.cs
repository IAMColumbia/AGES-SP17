using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerSelect : MonoBehaviour
{
    public int numberOfPlayers;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void OnTwoPlayerClick()
    {
        numberOfPlayers = 2;
        SceneManager.LoadScene("Instructions");
    }

    public void OnThreePlayerClick()
    {
        numberOfPlayers = 3;
        SceneManager.LoadScene("Instructions");
    }

    public void OnFourPlayerClick()
    {
        numberOfPlayers = 4;
        SceneManager.LoadScene("Instructions");
    }

    public int GetNumberOfPlayers()
    {
        return numberOfPlayers;
    }
}
