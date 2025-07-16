using UnityEngine;

public class SavePointTile : BaseTile
{
    public override TileType tileType => TileType.Save;

    public override void ExecuteTileBehavior()
    {
        base.ExecuteTileBehavior();
        // Логика сохранения игры
    }
}
