using UnityEngine;

public enum TileType
{
    Road,
    Quest,
    Save
}
public interface ITile
{
    float Length { get; }
    TileType tileType { get; }
    Collider PlayerEnterZone { get; }
    Vector3 Position { get; set; }
    bool IsActive { get; set; }
    void Initialize(int index);
}

