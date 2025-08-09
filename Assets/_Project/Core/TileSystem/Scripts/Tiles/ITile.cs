using System;
using UnityEngine;
public interface ITile
{
    TileType tileType { get; }
    int TileIndex { get; }
    GameObject TileGameObject { get; }
    void Initialize(int index);
    event Action<ITile> RequestNextTileAction;
    void RequestNextTile();
}

