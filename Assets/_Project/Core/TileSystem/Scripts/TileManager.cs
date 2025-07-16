using System;
using System.Collections.Generic;
using Zenject;

public class TileManager : ITileManager, IInitializable, IDisposable
{
    private readonly DiContainer _container;
    private readonly TileSettings _settings;
    private Dictionary<TileType, List<ITile>> _tiles = new Dictionary<TileType, List<ITile>>();
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
            var tile = _container.InstantiatePrefab(prefab).GetComponent<ITile>();
            tile.IsActive = false;
            var tileType = tile.tileType;
            if (!_tiles.ContainsKey(tileType))
            {
                _tiles[tileType] = new List<ITile>();
            }
            _tiles[tileType].Add(tile);
        }
    }

    public void OnPlayerEnteredTile(ITile tile)
    {
        
    }

    public void SpawnNextTile(TileType tileType)
    {
        ++_currentTileIndex;
        var tile = PickRandomTile(tileType);
        tile.Initialize(_currentTileIndex);
    }

    public void Dispose()
    {
        // Очистка ресурсов
    }


    private ITile PickRandomTile(TileType tileType)
    {
        var tileList = _tiles[tileType];
        int randomTileNumber = UnityEngine.Random.Range(0, tileList.Count);
        return tileList[randomTileNumber];

    }
}