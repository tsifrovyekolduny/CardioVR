using System;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour, ITile
{
    [SerializeField] private float _length = 26f;

    private int thisTileIndex;
    public float Length => _length;
    public int TileIndex => thisTileIndex;
    public abstract TileType tileType { get; }
    public GameObject TileGameObject => gameObject;
    public virtual void Initialize(int index)
    {
        transform.position = new Vector3(0, 0, index * Length);
        thisTileIndex = index;
    }

    public event Action<ITile> RequestNextTileAction;

    public void RequestNextTile()
    {
        Debug.Log("Делаю запрос");
        RequestNextTileAction?.Invoke(this);
    }
}