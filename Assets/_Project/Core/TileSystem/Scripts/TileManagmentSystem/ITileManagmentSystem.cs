public interface ITileManagmentSystem
{
    void SpawnNextTile(TileType tileType);
    bool CheckNextTileExistence(int tileIndex);
}