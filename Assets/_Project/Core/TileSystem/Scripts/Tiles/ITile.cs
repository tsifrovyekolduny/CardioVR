using UnityEngine;
public interface ITile
{
    float Length { get; }
    TileType tileType { get; }
    Collider PlayerEnterZone { get; }
    Vector3 Position { get; set; }
    bool IsActive { get; set; }
    int thisTileIndex { get; }
    void Initialize(int index, int tileIndex);
}

