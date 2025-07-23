using UnityEngine;

public class RoadTile : BaseTile
{
    public override TileType tileType => TileType.Road;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок коснулся зоны спавна");
            RequestNextTile(this);
        }
    }
}
