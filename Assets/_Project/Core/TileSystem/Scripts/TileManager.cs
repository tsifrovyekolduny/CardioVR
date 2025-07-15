using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Zenject;

public class TileManager : ITileManager, IInitializable, IDisposable
{
    private readonly DiContainer _container;
    private readonly TileSettings _settings;
    private readonly List<ITile> _activeTiles = new List<ITile>();
    private readonly Queue<ITile> _inactiveTiles = new Queue<ITile>();
    private int _currentTileIndex = 0;

    public TileManager(DiContainer container, TileSettings settings)
    {
        _container = container;
        _settings = settings;
    }

    public void Initialize()
    {
        PreloadTiles();
        SpawnInitialTiles();
    }

    private void PreloadTiles()
    {
        foreach (var prefab in _settings.TilePrefabs)
        {
            var tile = _container.InstantiatePrefab(prefab).GetComponent<ITile>();
            tile.IsActive = false;
            _inactiveTiles.Enqueue(tile);
        }
    }

    private void SpawnInitialTiles()
    {
        for (int i = 0; i < _settings.InitialTilesCount; i++)
        {
            SpawnNextTile();
        }
    }

    public void OnPlayerEnteredTile(ITile tile)
    {
        // Обработка входа игрока на тайл
        if (_activeTiles.Count > 0 && _activeTiles[0] == tile)
        {
            ReturnTileToPool(_activeTiles[0]);
            _activeTiles.RemoveAt(0);
        }

        // Вызов специфичных событий для тайла
        if (tile is ITileEvent tileEvent)
        {
            tileEvent.OnPlayerEnter();
        }
    }

    public void SpawnNextTile()
    {
        if (_inactiveTiles.Count == 0) return;

        var tile = _inactiveTiles.Dequeue();
        tile.Initialize(_currentTileIndex++, _activeTiles.Count > 0 ? _activeTiles[^1] : null);
        _activeTiles.Add(tile);
    }

    public void ReturnTileToPool(ITile tile)
    {
        tile.IsActive = false;
        _inactiveTiles.Enqueue(tile);
    }

    public void Dispose()
    {
        // Очистка ресурсов
    }
}