using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileManager : ITileManager, IInitializable, IDisposable
{
    private readonly DiContainer _container;
    private readonly TileSettings _settings;
    private Dictionary<TileType, List<GameObject>> _tilePrefabs = new Dictionary<TileType, List<GameObject>>();
    private int _currentTileIndex;

    public TileManager(DiContainer container, TileSettings settings)
    {
        _container = container;
        _settings = settings;
    }

    public void Initialize()
    {
        PreloadTiles();
        SpawnNextTile(TileType.Road);
    }


    private void PreloadTiles()
    {
        _currentTileIndex = 0;
        foreach (var prefab in _settings.TilePrefabs)
        {
            var tile = prefab.GetComponent<ITile>();
            if (tile == null) continue;

            var tileType = tile.tileType;
            if (!_tilePrefabs.ContainsKey(tileType))
            {
                _tilePrefabs[tileType] = new List<GameObject>();
            }
            _tilePrefabs[tileType].Add(prefab);
        }
    }

    public void ExecuteTileBehavior(ITile tile)
    {
        Debug.Log(_currentTileIndex);
        if (tile.tileType == TileType.Road && tile is ITileEvent tileEvent)
        {
            Debug.Log("Пытается создать тайл");
            tileEvent.NextTileTrigger();
        }
    }

    public void SpawnNextTile(TileType tileType)
    {
        if (!_tilePrefabs.ContainsKey(tileType))
        {
            Debug.LogError($"No prefabs found for tile type: {tileType}");
            return;
        }

        _currentTileIndex++;
        var randomPrefab = GetRandomPrefab(tileType);
        var tileInstance = _container.InstantiatePrefab(randomPrefab).GetComponent<ITile>();
        tileInstance.Initialize(_currentTileIndex, _currentTileIndex);
        tileInstance.IsActive = true;
    }

    public void Dispose()
    {
        // Очистка ресурсов
    }

    public bool CheckNextTileExistence(int tileIndex)
    {
        return _currentTileIndex <= tileIndex;
    }

    private GameObject GetRandomPrefab(TileType tileType)
    {
        var prefabs = _tilePrefabs[tileType];
        return prefabs[UnityEngine.Random.Range(0, prefabs.Count)];
    }
}