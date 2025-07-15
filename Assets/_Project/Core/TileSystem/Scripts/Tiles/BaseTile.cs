using UnityEngine;
using Zenject;

public abstract class BaseTile : MonoBehaviour, ITile, ITileEvent
{
    [SerializeField] private float _length = 26f;
    [SerializeField] private Collider _enterZone;

    public float Length => _length;
    public Collider PlayerEnterZone => _enterZone;
    public abstract TileType tileType { get; }
    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public bool IsActive { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    [Inject] protected ITileManager _tileManager;

    public virtual void Initialize(int index, ITile previousTile)
    {
        Position = new Vector3(0, 0, index * Length);
        IsActive = true;
    }

    public virtual void OnPlayerEnter()
    {
        _tileManager.OnPlayerEnteredTile(this);
    }

    public virtual void OpenExit() { }
    public virtual void OnExitOpen() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter();
        }
    }
}