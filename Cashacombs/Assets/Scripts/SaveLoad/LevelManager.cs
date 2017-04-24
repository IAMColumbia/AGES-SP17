using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LevelManager
{
    static List<GameObject> prefabsToSpawn;

    static BinaryFormatter binaryFormatter = new BinaryFormatter();
    static FileStream file;
    static Board board;
    //static List<GameObject> prefabsToSpawn = new List<GameObject>();


    public static void SetUpPlaceableObjectPrefabs()
    {
        prefabsToSpawn = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/PlaceableObjects"));
        Debug.Log("Prefabs Loaded in LevelManager: " + prefabsToSpawn.Count);
    }

    public static List<List<Tile>> LoadLevel(string levelName)
    {
        if (File.Exists(Application.persistentDataPath + "/levels/" + levelName + ".dat"))
        {
            file = File.Open(Application.persistentDataPath + "/levels/" + levelName + ".dat", FileMode.Open);

            //Load the level object
            Level levelDataToLoad = (Level)binaryFormatter.Deserialize(file);
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

                        //use the tileData's index to find the matching tile in our board, and add that tile to our lateUpdate list  //I HOPE THIS DOESN'T MIMIC THE DIAGONAL PROBLEM!!!!!
                        objectsForLateSetup.Add(placedObject.GetComponent<PlaceableObject>());
                        LateSetupCorrespondingTile.Add(boardTiles[(int)rowColumnIndex.x][(int)rowColumnIndex.y]);





                        //#region TODO Try to reduce this code and make it simpler.  Otherwise, you'll have to do something similar for EVERY GAMEOBJECT!!!
                        ////get the connectedObject from tileData
                        //PressurePlateData connectedObject = tilesInTileData[row][column].prefabOnTileName as PressurePlateData;
                        //TileData connectedTile = connectedObject.connectedTile;

                        ////create a vector2 to hold the row and column index
                        //Vector2 rowColumnIndex = Vector2.zero;

                        ////loop through the list of TileData to find the target tile
                        //for (int dataRow = 0; dataRow < tilesInTileData.Count; dataRow++)
                        //{
                        //    if(tilesInTileData[dataRow].Contains(connectedTile))
                        //    {
                        //        //save the tileData index to our Vector2
                        //        rowColumnIndex = new Vector2(dataRow, tilesInTileData[dataRow].IndexOf(connectedTile));
                        //        break;
                        //    }
                        //}

                        ////use the tileData's index to find the matching tile in our board, and add that tile to our lateUpdate list //I HOPE THIS DOESN'T MIMIC THE DIAGONAL PROBLEM!!!!!
                        //LateSetupCorrespondingTile.Add(boardTiles[(int)rowColumnIndex.x][(int)rowColumnIndex.y]);

                        //#endregion


                        //prepare the pressurePlate for lateSetup
                        //objectsForLateSetup.Add(boardTiles[row][column].GetComponent<PressurePlate>());
                        //LateSetupCorrespondingTile.Add()

                        //Get the tile that the Pressure plate is connected to
                        //Save this pressure plate to a list
                        //When we're finished setting up the board, find the corresponding tile to this pressurePlate
                        //Get the object on that corresponding tile
                        //Connect that object to the pressure plate

                        //boardTiles[row][column].ObjectOnTile.GetComponent<PressurePlate>().SetObjectToTrigger();
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
        Directory.CreateDirectory(Application.persistentDataPath + "/levels");
        file = File.Open(Application.persistentDataPath + "/levels/" + levelName + ".dat", FileMode.OpenOrCreate);

        Level levelDataToSave = new Level(tiles);
        binaryFormatter.Serialize(file, levelDataToSave);
        file.Close();
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


