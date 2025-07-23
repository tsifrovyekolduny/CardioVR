using System;
using UnityEngine;
public interface ITile
{
    float Length { get; }
    TileType tileType { get; }
    Vector3 Position { get; set; }
    bool IsActive { get; set; }
    int TileIndex { get; }
    GameObject TileGameObject { get; }
    void Initialize(int index);
    event Action<ITile> RequestNextTileAction;
    void RequestNextTile(ITile requestingTile);
}

