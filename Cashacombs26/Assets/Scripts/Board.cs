using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* THE PURPOSE OF THIS CLASS IS TO: GENERATE AND KEEP TRACK OF THE BOARD
 * 1) Generate a grid of tiles
 * 2) change size of grid (Add rows and columns)
 * 3) Keep track of each tile on the board (acts like a tile manager)
 *      BUT: this class should have no knowledge of the tile's contents
 * 4) When loading a level, this class should generate each space?????????????? (Maybe make a "Load Level" class)
 * 5) Maybe even generate path for walking and enemies
 */

public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    int boardRows = 5;
    int boardColumns = 5;

    /* Each list represents a row.  Each row should contain the same amount of columns
     * TODO: having this as static might be really, really gross / problematic.
     *      It might cause problems when loading and unloading levels, since there can only every be one list of tiles among all boards.
     *      Solution 1: Give the controller a reference to the board.  It might need it.
     *      Solution 2: Make boardTiles non-static, but make a separate static list that can be used for static methods (and maybe make a non-static method that sets the static list to the non-static one)
     *      Solution 3: Make a separate class that decouples the board and the controller
     */
    List<List<Tile>> boardTiles = new List<List<Tile>>(); //Row, Column          

    // Use this for initialization
    void Start()
    {
        LevelManager.SetUpPlaceableObjectPrefabs();
        GenerateGrid();
    }

    /// <summary>
    /// Assumes that the board has already been fully constructed AND
    /// Assumes that each tile already knows what's supposed to be on that tile
    /// </summary>
    public void BuildInGameLevel()
    {
        Debug.Log("Started Game");
        PlaceableObject.currentObjectInteractedWith = null;

        for(int row = 0; row < boardTiles.Count; row++)
        {
            for(int column = 0; column < boardTiles[row].Count; column++)
            {
                Tile currentTile = boardTiles[row][column];
                currentTile.GameInit(new Vector2(row, column));
            }
        }
    }

    public void BuildInGameLevel(List<List<Tile>> tilesToBuild)
    {
        boardTiles = tilesToBuild;

        Debug.Log("Started Game");
        StateManager.gameState = StateManager.GameState.IN_GAME;

        for (int row = 0; row < boardTiles.Count; row++)
        {
            for (int column = 0; column < boardTiles[row].Count; column++)
            {
                Tile currentTile = boardTiles[row][column];
                currentTile.GameInit(new Vector2(row, column));
            }
        }
    }

    void GenerateGrid()
    {
        for (int row = 0; row < boardRows; row++)
        {
            List<Tile> tilesInRow = new List<Tile>();
            for (int column = 0; column < boardColumns; column++)
            {
                //create the new tile and save it in the row
                GameObject newTileObject = Instantiate(tilePrefab, new Vector3(row * 2, 0, column * 2), Quaternion.identity, transform);

                Tile newTileScript = newTileObject.GetComponent<Tile>();
                newTileScript.Init(new Vector2(row, column));

                tilesInRow.Add(newTileScript);
            }
            boardTiles.Add(tilesInRow);
        }
    }

    /// <summary>
    /// Generate a grid with the given dimensions
    /// </summary>
    /// <param name="rows">How many rows are in the grid</param>
    /// <param name="columns">How many columns in the grid</param>
    /// <returns>2D list of tiles in the board</returns>
    public List<List<Tile>> GenerateGrid(int rows, int columns)
    {
        for (int row = 0; row < rows; row++)
        {
            List<Tile> tilesInRow = new List<Tile>();
            for (int column = 0; column < columns; column++)
            {
                //create the new tile and save it in the row
                GameObject newTile = Instantiate(tilePrefab, new Vector3(row * 2, 0, column * 2), Quaternion.identity, transform);
                Tile t = newTile.GetComponent<Tile>();
                t.Init(new Vector2(row, column));
                tilesInRow.Add(newTile.GetComponent<Tile>());
            }
            boardTiles.Add(tilesInRow);
        }

        return boardTiles;
    }



    public void AddRow()
    {
        List<Tile> newRowList = new List<Tile>();

        //loop through all the columns (get the length of the first row, which is how many columns there are (assuming all rows have the same amount of columns)
        for (int column = 0; column < boardTiles[0].Count; column++)
        {
            GameObject newTileObject = Instantiate(tilePrefab, new Vector3((boardTiles.Count) * 2, 0, column * 2), Quaternion.identity, transform);
            Tile newTile = newTileObject.GetComponent<Tile>();
            newTile.Init(new Vector2(boardTiles[0].Count, column));
            newRowList.Add(newTile);
        }
        boardTiles.Add(newRowList);
    }

    public void AddColumn()
    {
        //loop through all the rows (get how many lists there are, since each list represnts a row)
        for (int row = 0; row < boardTiles.Count; row++)
        {
            GameObject newTileObject = Instantiate(tilePrefab, new Vector3(row * 2, 0, boardTiles[row].Count * 2), Quaternion.identity, transform);
            Tile newTile = newTileObject.GetComponent<Tile>();
            newTile.Init(new Vector2(row, boardTiles[row].Count));

            //add a new tile to the end of the row (thus, adding a column)
            boardTiles[row].Add(newTile);
        }
    }

    public void RemoveRow()
    {
        if (boardTiles.Count > 1)
        {
            //destroy each tile in the last row (list)
            foreach (Tile tileToDestroy in boardTiles[boardTiles.Count - 1])
            {
                tileToDestroy.DestroySelf();
            }

            //destroy the last row (list)
            boardTiles.Remove(boardTiles[boardTiles.Count - 1]);
        }
    }

    public void RemoveColumn()
    {
        if (boardTiles[boardTiles.Count - 1].Count > 1)
        {
            //loop through each row and destroy the last tile in each
            foreach (List<Tile> row in boardTiles)
            {
                Tile tileToDestroy = row[row.Count - 1];
                tileToDestroy.DestroySelf();
                //remove the tile from the list
                row.Remove(tileToDestroy);
            }
        }
    }

    void ClearBoard()
    {
        for (int row = boardTiles.Count - 1; row >= 0; row--)
        {
            for (int column = boardTiles[row].Count - 1; column >= 0; column--)
            {
                boardTiles[row][column].DestroySelf();
            }
        }

        boardTiles.Clear();

        EndChest.endGoal = null;
        PlayerStart.playerStart = null;
    }


    public bool IsTargetTileAdjacent(Tile currentTile, Tile targetTile)
    {
        /* targetTile is adjecent to the targetTile if its index is
         * 1) one greater or one less than the currentTile's row index
         * 2) one greater or one less than the currentTile's column index
         */

        Vector2 currentTileIndex, targetTileIndex;

        currentTileIndex = GetTileIndex(currentTile);
        targetTileIndex = GetTileIndex(targetTile);

        //if both currentTile and targetTile rows have been found...
        if (currentTileIndex != new Vector2(-1, -1) && targetTileIndex != new Vector2(-1, -1))
        {
            //subtract the two vectors
            Vector2 differenceBetween = new Vector2(Mathf.Abs(currentTileIndex.x - targetTileIndex.x), Mathf.Abs(currentTileIndex.y - targetTileIndex.y));

            //it'll only ever be positive, so no need to check down or left
            if (differenceBetween == Vector2.up || differenceBetween == Vector2.right)
                return true;
        }
        return false;
    }



    Vector2 GetTileIndex(Tile tile)
    {
        /* A) the row is index of the rowList
         * B) the column is the index of the tile in the rowList */

        List<Tile> tileRow = null;

        //loop through each row in the list and see if that row contains the tile
        foreach (List<Tile> rowList in boardTiles)
        {
            if (rowList.Contains(tile))
            {
                tileRow = rowList;
                break;              //if we found the list, we can stop looping
            }
        }

        //if the tileRow was found, return the index of the tile (we also know the tile exists in that list).  Otherwise, return a (-1, -1) vector
        return tileRow != null ? new Vector2(boardTiles.IndexOf(tileRow), tileRow.IndexOf(tile)) : new Vector2(-1, -1);
    }



    public Player GetPlayerFromBoard()
    {
        for (int row = 0; row < boardTiles.Count; row++)
        {
            for (int column = 0; column < boardTiles[row].Count; column++)
            {
                Tile currentTile = boardTiles[row][column];

                if (currentTile.CharacterOnTile && currentTile.CharacterOnTile.GetComponent<Player>())
                {
                    return currentTile.CharacterOnTile.GetComponent<Player>();
                }
            }
        }

        return null;
    }



    /// <summary>
    /// Moves the target directly to a tile
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetTile"></param>
    /// <returns>Whether the action could be successfully completed or not</returns>
    public bool AttemptActivateAndMoveTargetToTile(GameObject target, Tile targetTile)
    {
        if(StateManager.playerState != StateManager.PlayerState.DEAD && target.GetComponent<IMovable>() != null)
        {
            return target.GetComponent<IMovable>().GoToTile(targetTile);
        }

        return false;
    }


    /// <summary>
    /// Moves the target in a specified direction
    /// </summary>
    /// <param name="target"></param>
    /// <param name="direction"></param>
    /// <returns>Whether the action could be successfully completed or not</returns>
    public bool AttemptMoveTargetToTile(GameObject target, Vector3 direction)
    {
        if (StateManager.playerState != StateManager.PlayerState.DEAD)
        {
            if (target.GetComponent<IMovable>() != null)
            {

                IMovable movableObject = target.GetComponent<IMovable>();

                Tile targetTile = null;
                Vector2 tileIndex = GetTileIndex(movableObject.GetCurrentTile());

                if (tileIndex == new Vector2(-1, -1))
                    return false;

                //becuase of that 0.00000001 off, I have to brute force this                //DIAGONAL PROBLEM
                if (direction.x > 0.5f)
                    direction = Vector3.right;
                else if (direction.x < -0.5f)
                    direction = Vector3.left;
                else if (direction.z > 0.5f)
                    direction = Vector3.forward;
                else if (direction.z < -0.5f)
                    direction = -Vector3.forward;

                //consistently breaks if...
                //block is placed on (4, 3) and, spawn in front, rotate clockwise, pushing forward each time
                //if (on row 3), spawn in front move to the right side of the block and pull backwards

                Vector2 newTileIndex = tileIndex + new Vector2(direction.x, direction.z);   //NOTE: 

                //Debug.Log(newTileIndex);                                                                        //DIAGONAL PROBELM \/

                if (newTileIndex.x < boardTiles.Count && newTileIndex.x >= 0 &&
                    newTileIndex.y < boardTiles[(int)newTileIndex.x].Count && newTileIndex.y >= 0)
                {
                    targetTile = boardTiles[(int)newTileIndex.x][(int)newTileIndex.y];
                    return movableObject.GoToTile(targetTile);
                }
                else
                {
                    Debug.Log("Target Tile not on board!");
                    return false;
                }                                                                                              //DIAGONAL PROBLEM /\
            }
        }
        return false;
    }

    public void SaveBoard(string levelName)
    {
        if (levelName != "")
            LevelManager.SaveLevel(levelName, boardTiles);

    }

    public void LoadBoard(string levelToLoad)
    {
        //destroy the existing board
        ClearBoard();

        //load the new one
        boardTiles = LevelManager.LoadLevel(levelToLoad);
        BuildInGameLevel();
    }
}
