using UnityEngine;
using Zenject;

public class RoadTile : BaseTile
{
    public override TileType tileType => TileType.Road;

    [Inject] private IOperator _operator;

    override public void  ExecuteTileBehavior()
    {

        if (!_tileManager.CheckNextTileExistence(TileIndex))
        {
            return;
        }

        base.ExecuteTileBehavior();
 
        _tileManager.SpawnNextTile(TileType.Quest);
    }
}
