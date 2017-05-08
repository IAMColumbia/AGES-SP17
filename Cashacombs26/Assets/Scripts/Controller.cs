using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* THE PURPOSE OF THIS CLASS IS TO: DETERMINE MOUSE INPUTS FROM THE PLAYER
 * 1) What did you click on? (which space / tile?)
 * 2) What does that mean in this context? (Am I in the game?  Am I in the editor?)
 * 3) This is currently acting as a player controller as well... (I should encapsulate that... BAD NATHAN)
 */

public class Controller : MonoBehaviour
{
    [SerializeField] LayerMask TileLayer;

    [HideInInspector] public GameObject selectedObjectInEditor; //set in GUIPlaceableItem script
    public float mouseSensitivity = 5;

    Player player;
    Board board;
    Tile selectedTile;

    string currentLevel = "";

    void Start()
    {
        board = GameObject.FindObjectOfType<Board>().GetComponent<Board>();
    }

    #region Methods for Buttons
    public void StartGame(string levelToLoad)
    {
        if (board)
        {
            StateManager.gameState = StateManager.GameState.IN_GAME;
            StateManager.playerState = StateManager.PlayerState.WALKING;

            board.LoadBoard(levelToLoad);

            player = board.GetPlayerFromBoard();

            currentLevel = levelToLoad;
        }
        else
            throw new System.Exception("No board could be found in the scene!");
    }

    public void RestartGame()
    {
        if (currentLevel != "")
        {
            StartGame(currentLevel);
        }
        else
        {
            throw new System.Exception("The variable \"currentLevel\" inside \"Controller\" is null!");
        }
    }
    #endregion

    private void Update()
    {
        if(Input.GetButtonDown("Selection"))
        {
            selectedTile = DetermineClickedTile();

            if (selectedTile)
            {
                DetermineAction();
            }
        }

        PanCamera();
    }

    //ew?
    private void DetermineAction()
    {
        switch (StateManager.gameState)
        {
            case StateManager.GameState.IN_GAME:
            {
                    //IF WE'RE IN THE GAME!

                switch (StateManager.playerState)
                {

                    case StateManager.PlayerState.WALKING: //nested switch
                    {
                        //if the tile we're trying to move to is adjacent
                        if (board.IsTargetTileAdjacent(player.GetCurrentTile(), selectedTile))
                        {
                            board.AttemptActivateAndMoveTargetToTile(player.gameObject, selectedTile);
                        }
                        break;
                    }
                    case StateManager.PlayerState.DEAD:
                        {
                            Debug.Log("Player has died and shouldn't be moving");
                            break;
                        }
                    case StateManager.PlayerState.INTERACTING: //nested switch
                    {
                        if(selectedTile != null && selectedTile.ObjectOnTile != PlaceableObject.currentObjectInteractedWith.gameObject)
                        {
                            Tile.DeselectCurrentObject();
                        }
                        break;
                    }
                    case StateManager.PlayerState.WON_LEVEL:
                    {
                        Debug.Log("Player has won!  Shouldn't be moving now!");
                        break;
                    }

                }

                break;
            }


            case (StateManager.GameState.LEVEL_EDITOR):
            {
                    //IF WE'RE IN THE LEVEL EDITOR!

                //Info from GUIPlacableItem -> CONTROLLER -> TILE
                if (selectedObjectInEditor != null)
                {
                    selectedTile.SetItemToTile(selectedObjectInEditor);
                }
                else
                {
                    selectedTile.RemoveObjectFromTile();
                }
                break;
            }


            case (StateManager.GameState.PAUSED):
            {
                break;
            }
        }
    }

    private Tile DetermineClickedTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 99, TileLayer))
        {
            return hit.collider.gameObject.GetComponent<Tile>();
        }
        else
        {
            return null;
        }
    }

    private void PanCamera()
    {

        if(Input.GetKey(KeyCode.Mouse1))
        {
            float mouseScrollHorizontal = Input.GetAxis("MouseHorizontal");
            float mouseScrollVertical = Input.GetAxis("MouseVertical");

            Camera.main.transform.localPosition += new Vector3(mouseScrollHorizontal, mouseScrollVertical, mouseScrollHorizontal) * mouseSensitivity * Time.deltaTime;
        }
    }
}
