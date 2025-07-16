using System;
using UnityEngine;
using Zenject;

public abstract class BaseTile : MonoBehaviour, ITile, ITileEvent
{
    [SerializeField] private float _length = 26f;
    [SerializeField] private Collider _enterZone;

    public int TileIndex;
    public float Length => _length;
    public int thisTileIndex => TileIndex;
    public Collider PlayerEnterZone => _enterZone;
    public abstract TileType tileType { get; }
    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public bool IsActive { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    [Inject] protected ITileManager _tileManager;

    public virtual void Initialize(int index, int tileIndex)
    {
        Position = new Vector3(0, 0, index * Length);
        IsActive = true;
        TileIndex = tileIndex;
    }

    public virtual void ExecuteTileBehavior()
    {
        _tileManager.ExecuteTileBehavior(this);
    }

    public virtual void OpenExit() { }
    public virtual void NextTileTrigger() { }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок епты");
            ExecuteTileBehavior();
        }
    }
}