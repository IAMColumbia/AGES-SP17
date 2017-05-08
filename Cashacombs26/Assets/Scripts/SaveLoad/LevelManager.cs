using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LevelManager
{
    static string filepath = Application.persistentDataPath + "/levels/";
    const string extension = ".dat";

    static List<GameObject> prefabsToSpawn;

    static BinaryFormatter binaryFormatter = new BinaryFormatter();
    static FileStream file;
    static Board board;


    public static void SetUpPlaceableObjectPrefabs()
    {
        prefabsToSpawn = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/PlaceableObjects"));
        Debug.Log("Prefabs Loaded in LevelManager: " + prefabsToSpawn.Count);
    }

    public static List<List<Tile>> LoadLevel(string levelName)
    {
        if (File.Exists(filepath + levelName + extension))
        {
            file = File.Open(filepath + levelName + extension, FileMode.Open);

            //Load the level object
            Level levelDataToLoad = (Level)binaryFormatter.Deserialize(file);
            Debug.Log(levelDataToLoad);
            file.Close();


            //Find the board
            board = GameObject.FindObjectOfType<Board>();

            //tell the board to generate a grid the size of the tiles
            List<List<TileData>> tilesInTileData = levelDataToLoad.GetTileData();
            List<List<Tile>> boardTiles = board.GenerateGrid(tilesInTileData.Count, tilesInTileData[0].Count);

            List<PlaceableObject> objectsForLateSetup = new List<PlaceableObject>();
            List<Tile> LateSetupCorrespondingTile = new List<Tile>();

            //Loop through all the tiles
            for (int row = 0; row < tilesInTileData.Count && row < boardTiles.Count; row++)
            {
                for (int column = 0; column < tilesInTileData[row].Count && column < boardTiles[row].Count; column++)
                {
                    //Set up the object on each tile
                    PlaceableObjectData typeOfObjectData = tilesInTileData[row][column].prefabOnTileName;

                    //Read the ID from the TileData
                    if (typeOfObjectData is EndChestData)
                    {
                        boardTiles[row][column].SetItemToTile(FindGameObjectInPrefabList<EndChest>());
                    }
                    else if (typeOfObjectData is PlayerStartData)
                    {
                        boardTiles[row][column].SetItemToTile(FindGameObjectInPrefabList<PlayerStart>());
                    }
                    else if (typeOfObjectData is PressurePlateData)
                    {
                        //place the pressurePlate on the tile
                        GameObject placedObject = boardTiles[row][column].SetItemToTile(FindGameObjectInPrefabList<PressurePlate>());


                        //get the row and column index from the TileData
                        PressurePlateData connectedObject = tilesInTileData[row][column].prefabOnTileName as PressurePlateData;
                        Vector2 rowColumnIndex = new Vector2(connectedObject.row, connectedObject.column);

                        if (rowColumnIndex != new Vector2(-1, -1))
                        {
                            //use the tileData's index to find the matching tile in our board, and add that tile to our lateUpdate list  //I HOPE THIS DOESN'T MIMIC THE DIAGONAL PROBLEM!!!!!
                            objectsForLateSetup.Add(placedObject.GetComponent<PlaceableObject>());
                            LateSetupCorrespondingTile.Add(boardTiles[(int)rowColumnIndex.x][(int)rowColumnIndex.y]);
                        }
                    }
                    else if (typeOfObjectData is PushableBlockData)
                    {
                        boardTiles[row][column].SetItemToTile(FindGameObjectInPrefabList<PushableBlock>());
                    }
                    else if (typeOfObjectData is SpikeTrapData)
                    {
                        boardTiles[row][column].SetItemToTile(FindGameObjectInPrefabList<SpikeTrap>());
                    }
                    else if (typeOfObjectData is WallData)
                    {
                        boardTiles[row][column].SetItemToTile(FindGameObjectInPrefabList<Wall>());
                    }
                }
            }

            //loop through all the tiles that need a late setup
            //aka: objects that require the board to be set up first, then they can be fully created
            for (int i = 0; i < objectsForLateSetup.Count && i < LateSetupCorrespondingTile.Count; i++)
            {
                objectsForLateSetup[i].LateSetup(LateSetupCorrespondingTile[i]);
            }

            return boardTiles;
        }

        return null;
    }

    public static void SaveLevel(string levelName, List<List<Tile>> tiles)
    {
        if (!Directory.Exists(filepath))
        {
            FirstTimeSetupAndLoadDefaultLevels();
        }

        file = File.Open(filepath + levelName + extension, FileMode.OpenOrCreate);

        Level levelDataToSave = new Level(tiles);
        binaryFormatter.Serialize(file, levelDataToSave);
        file.Close();
    }


    /// <summary>
    /// Attempts to delete the level with the given name
    /// </summary>
    /// <param name="levelToDelete">The name of the file to delete (no extension)</param>
    /// <returns>Returns true if the level was successfully deleted</returns>
    public static bool DeleteLevel(string levelToDelete)
    {
        if (!Directory.Exists(filepath))
        {
            FirstTimeSetupAndLoadDefaultLevels();
        }

        if(levelToDelete != "" && File.Exists(filepath + levelToDelete + extension))
        {
            File.Delete(filepath + levelToDelete + extension);
            return true;
        }

        return false;
    }



    //Note that the resources folder is memory intensive
    public static void FirstTimeSetupAndLoadDefaultLevels()
    {
        Directory.CreateDirectory(filepath);

        Debug.Log("Loading default levels");
        TextAsset[] levels = Resources.LoadAll<TextAsset>("PrebuiltLevels");    //load the levels from the resources folders (must be of ".byte" extension)

        //write the bytes from each of those files into a new ".dat" file (which will be located in the "persistentData" folder)
        //see: https://docs.unity3d.com/Manual/class-TextAsset.html and look for "BinaryData" header
        for (int level = 1; level <= levels.Length; level++)
        {
            File.WriteAllBytes(filepath + "Level " + level + extension, levels[level - 1].bytes);
        }
    }



    /// <summary>
    /// Returns a gameObject prefab with the given type from the list
    /// </summary>
    /// <typeparam name="T">Must be a type of PlaceableObject</typeparam>
    /// <returns></returns>
    static GameObject FindGameObjectInPrefabList<T>() where T : PlaceableObject
    {
        foreach (GameObject go in prefabsToSpawn)
        {
            if (go.GetComponent<PlaceableObject>() is T) //downcast the Placeable
            {
                return go;
            }
        }

        throw new System.Exception("Your new prefab was not added to the 'PlacableObjectPrefabs' list inside the 'levelManager' script!");
    }
}


