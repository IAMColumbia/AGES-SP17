using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public List<List<TileData>> tilesInLevel = new List<List<TileData>>();

    public Level(List<List<Tile>> allTiles)
    {
        TileData newTileData;
        for (int row = 0; row < allTiles.Count; row++)
        {
            List<TileData> tileDataRow = new List<TileData>();
            for (int column = 0; column < allTiles[row].Count; column++)
            {
                newTileData = allTiles[row][column].GenerateTileData();
                tileDataRow.Add(newTileData);
            }
            tilesInLevel.Add(tileDataRow);
        }
    }

    public List<List<TileData>> GetTileData()
    {
        return tilesInLevel;
    }
}
