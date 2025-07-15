using System.Collections.Generic;

public interface ITileManager
{
    void Initialize();
    void OnPlayerEnteredTile(ITile tile);
    void SpawnNextTile();
    void ReturnTileToPool(ITile tile);
}