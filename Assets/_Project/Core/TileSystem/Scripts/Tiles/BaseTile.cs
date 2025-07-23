using System;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour, ITile
{
    [SerializeField] private float _length = 26f;

    private int thisTileIndex;
    public float Length => _length;
    public int TileIndex => thisTileIndex;
    public abstract TileType tileType { get; }
    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public bool IsActive { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    public GameObject TileGameObject => gameObject;

    public virtual void Initialize(int index)
    {
        Position = new Vector3(0, 0, index * Length);
        IsActive = true;
        thisTileIndex = index;
    }

    public event Action<ITile> RequestNextTileAction;

    public void RequestNextTile(ITile requestingTile)
    {
        Debug.Log("Делаю запрос");
        RequestNextTileAction?.Invoke(this);
    }
}