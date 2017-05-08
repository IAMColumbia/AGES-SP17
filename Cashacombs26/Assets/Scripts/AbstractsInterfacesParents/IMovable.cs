

public interface IMovable
{
    /// <summary>
    /// Moves the object directly to the specified tile
    /// </summary>
    /// <param name="tile"></param>
    /// <returns>Whether the object has moved or not</returns>
    bool GoToTile(Tile tile);

    Tile GetCurrentTile();
}
