using UnityEngine;

public class RoadTile : BaseTile
{
    public override TileType tileType => TileType.Road;

    override public void NextTileTrigger()
    {
        if (!_tileManager.CheckNextTileExistence(TileIndex))
        {
            return;
        }
 
        _tileManager.SpawnNextTile(TileType.Quest);
    }
}
