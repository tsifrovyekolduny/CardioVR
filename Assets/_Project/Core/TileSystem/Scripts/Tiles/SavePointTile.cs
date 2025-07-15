using UnityEngine;

public class SavePointTile : BaseTile
{
    public override TileType tileType => TileType.Save;

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        // Логика сохранения игры
    }
}
