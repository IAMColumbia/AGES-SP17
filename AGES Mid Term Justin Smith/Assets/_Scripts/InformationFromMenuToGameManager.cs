using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InformationFromMenuToGameManager : MonoBehaviour
{

    [SerializeField]
    GameObject gameObjectHoldingScriptToSaveData;
    [SerializeField]
    GameObject players3Button;
    [SerializeField]
    GameObject players4Button;

    int sceneToLoadIndex = 1;
    bool haveAllPlayersSelectedColors;
    bool[] playersThatHaveSelectedColors;
    public bool[] PlayersThatHaveSelectedColors
    {
        get { return playersThatHaveSelectedColors; }
    }
    int numberOfPlayersToLoad;
    public int NumberOfPlayersToLoad
    {
        get { return numberOfPlayersToLoad; }
        set { numberOfPlayersToLoad = value; }
    }
    Color[] playersColorChoices = new Color[4];
    public Color[] PlayersColorChoices
    {
        get { return playersColorChoices; }
    }

    public void SetPlayerColor(int playerNumber, Color playerColorSelected)
    {
        
        playersColorChoices[playerNumber - 1] = playerColorSelected;
        playersThatHaveSelectedColors[playerNumber - 1] = true;
    }

	void Start ()
    {
        DontDestroyOnLoad(gameObjectHoldingScriptToSaveData);
        haveAllPlayersSelectedColors = false;
        //players3Button.SetActive(true);
        //players4Button.SetActive(true);
    }

    void Update()
    {
        if (playersThatHaveSelectedColors != null)
        {
            foreach (bool hasPlayerSelectedColor in playersThatHaveSelectedColors)
            {
                if (hasPlayerSelectedColor)
                    haveAllPlayersSelectedColors = true;
                else
                {
                    haveAllPlayersSelectedColors = false;
                    break;
                } 
            }
        }

        if (haveAllPlayersSelectedColors)
            SceneManager.LoadScene(sceneToLoadIndex);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }

    public void OnPlayerSelect(int numberOfPlayers)
    {
        numberOfPlayersToLoad = numberOfPlayers;

        if (numberOfPlayers == 2)
        {
            players3Button.SetActive(false);
            players4Button.SetActive(false);
            playersThatHaveSelectedColors = new bool[2];
        }
        else if (numberOfPlayers == 3)
        {
            players4Button.SetActive(false);
            playersThatHaveSelectedColors = new bool[3];
        }
        else
            playersThatHaveSelectedColors = new bool[4];

    }
	
}
