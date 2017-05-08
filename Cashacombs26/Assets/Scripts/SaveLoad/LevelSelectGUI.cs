using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectGUI : MonoBehaviour
{
    public static Toggle currentToggle;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Button deleteButton;

    [SerializeField] ToggleAndName[] toggles;

    [System.Serializable]
    struct ToggleAndName
    {
        public Toggle toggle;
        public Text levelNameTextBox;

    }

    //Which set of levels are we currently showing?
    //NOTE: MY LOGIC DOES NOT MATCH MY VARIABLE NAME
    //(I began doing it one way, then I realized my logic was backwards.  "Start" is actually the "Beginning"
    //The true start is: "Start - 2 * entriesPerSlide". Oops...
    int currentEntryStartIndex = 0;
    int levelEntriesToShowPerSlide;


    int currentEntryEndIndex
    {
        get { return currentEntryStartIndex + levelEntriesToShowPerSlide; }
    }


    const string extension = ".dat";

    List<string> levelNames = new List<string>();
    


    void Start()
    {
        levelEntriesToShowPerSlide = toggles.Length;
        Init();
    }

    public void Init()
    {
        DeactivateOldToggles();

        string filepath = Application.persistentDataPath + "/levels/";

        //if we haven't run this code yet, create a place for our levels and load our default levels
        if (!Directory.Exists(filepath))
        {
            LevelManager.FirstTimeSetupAndLoadDefaultLevels();
        }

        DirectoryInfo directoryInfo = new DirectoryInfo(filepath);
        FileInfo[] files = directoryInfo.GetFiles("*" + extension); //get all files in the folder with our target extension

        //Clear the list and repopulate it
        levelNames.Clear();
        foreach (FileInfo file in files)
        {
            //get the name of the file and remove the extension (the .dat at the end)
            string newLevelName = file.Name;
            newLevelName = newLevelName.Remove(newLevelName.Length - extension.Length);

            //add the level name to the list
            levelNames.Add(newLevelName);
        }

        currentToggle = null;
        currentEntryStartIndex = 0;
        ShowNextEntries();


        //hide delete button if we're in the game
        if(StateManager.previousGameState != StateManager.GameState.LEVEL_EDITOR && StateManager.gameState != StateManager.GameState.LEVEL_EDITOR)
        {
            deleteButton.gameObject.SetActive(false);
        }
        else
        {
            deleteButton.gameObject.SetActive(true);
        }
    }





    void DeactivateOldToggles()
    {
        foreach(ToggleAndName toggle in toggles)
        {
            toggle.toggle.gameObject.SetActive(false);
        }
    }

    void UpdateSlideButtonsVisibility()
    {
        //set the left button active or inactive if there are entries from the last slide
        leftButton.gameObject.SetActive(currentEntryStartIndex - 2*levelEntriesToShowPerSlide >= 0);

        //set the right button active or inactive if there are more entires on the next slide
        rightButton.gameObject.SetActive(currentEntryStartIndex < levelNames.Count);
    }



    #region Methods for buttons

    public void ShowNextEntries() //click "Right arrow" button
    {
        //Set all the old toggles inactive
        DeactivateOldToggles();

        //continue looping until we either hit the end of the list (OR) we've shows the max amount of entries on this slide
        for(int x = 0; x + currentEntryStartIndex < levelNames.Count && x < levelEntriesToShowPerSlide; x++)
        {
            toggles[x].levelNameTextBox.text = levelNames[currentEntryStartIndex + x];
            toggles[x].toggle.gameObject.SetActive(true);

        }

        //whether or not we actually looped through an entire set of entries, we still want to increment the start index by the full amount
        //this will keep entry sets in order
        currentEntryStartIndex += levelEntriesToShowPerSlide;

        UpdateSlideButtonsVisibility();
    }

    public void ShowPreviousEntries() //click "left arrow" button
    {
        //Set all the old toggles inactive
        DeactivateOldToggles();

        //set the starting index one set backwards
        currentEntryStartIndex -= levelEntriesToShowPerSlide;

        for (int x = 0; x < levelEntriesToShowPerSlide; x++)
        {
            toggles[x].levelNameTextBox.text = levelNames[currentEntryStartIndex - levelEntriesToShowPerSlide + x];
            toggles[x].toggle.gameObject.SetActive(true);
        }

        UpdateSlideButtonsVisibility();
    }


    public void SetCurrentToggle(Toggle desiredToggle) //click on a toggle
    {
        currentToggle = desiredToggle;
    }

    public void LoadSelectedLevel() //click "Load" button
    {
        if(currentToggle)
        {
            string targetLevelName = "";

            foreach (ToggleAndName toggleStruct in toggles)
            {
                if (currentToggle == toggleStruct.toggle)
                {
                    targetLevelName = toggleStruct.levelNameTextBox.text;
                }
            }

            if (targetLevelName != "")
            {
                //Load the level in the level editor if we were in the level editor before pausing.  Otherwise, load the level in game.  (That's a mouthful!)
                if (StateManager.previousGameState == StateManager.GameState.LEVEL_EDITOR)
                {
                    //load the level
                    Board board = FindObjectOfType<Board>();
                    board.LoadBoard(targetLevelName);
                    GetComponentInParent<GUIManager>().TurnOnLevelEditorCanvas();
                }
                else
                {
                    //start the game
                    Controller controller = FindObjectOfType<Controller>();
                    controller.StartGame(targetLevelName);
                    GetComponentInParent<GUIManager>().TurnOnInGameCanvas();
                }

                this.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("You must have a level selected first!");
        }
    }



    public void DeleteSelectedLevel()
    {
        string targetLevelName = "";

        if (currentToggle)
        {
            foreach (ToggleAndName toggleStruct in toggles)
            {
                if (currentToggle == toggleStruct.toggle)
                {
                    targetLevelName = toggleStruct.levelNameTextBox.text;
                }
            }

            if (targetLevelName != "")
            {
                if (LevelManager.DeleteLevel(targetLevelName)) { Init(); }
            }
        }
    }
    #endregion
}
