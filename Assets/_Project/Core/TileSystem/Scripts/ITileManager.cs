using System;
using System.Collections.Generic;

public interface ITileManager
{
    void Initialize();
    void HandleTileSpawnRequest(ITile tile);
    void SpawnNextTile(TileType tileType);
    bool CheckNextTileExistence(int tileIndex);
}