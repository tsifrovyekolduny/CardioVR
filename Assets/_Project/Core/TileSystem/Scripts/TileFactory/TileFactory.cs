using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileFactory : ITileFactory
{
    private readonly DiContainer _container;
    private readonly Dictionary<TileType, List<ITile>> _tilePrefabs;

    [Inject]
    public TileFactory(DiContainer container, TilePrefabsObject tilePrefabsObject)
    {
        _container = container;
        _tilePrefabs = PreloadTiles(tilePrefabsObject);
    }

    private Dictionary<TileType, List<ITile>> PreloadTiles(TilePrefabsObject tilePrefabsObject)
    {
        var temporaryTileDictionary = new Dictionary<TileType, List<ITile>>();
        foreach (GameObject prefab in tilePrefabsObject.TilePrefabs)
        {
            ITile tile = prefab.GetComponent<ITile>();

            if (tile == null)
            {
                Debug.LogError($"{tile} не обладает необходимым интерфейсом");
                continue;
            }

            TileType tileType = tile.tileType;
            if (!temporaryTileDictionary.ContainsKey(tileType))
            {
                temporaryTileDictionary[tileType] = new List<ITile>();
            }
            temporaryTileDictionary[tileType].Add(tile);
        }

        return temporaryTileDictionary;
    }

    public ITile CreateTile(TileType tileType, int index)
    {
        if (!_tilePrefabs.ContainsKey(tileType))
        {
            throw new MissingReferenceException($"Не было загружено тайлов типа {tileType}");
        }

        var randomTilePrefab = GetRandomTile(tileType);
        var tileInstance = _container.InstantiatePrefabForComponent<ITile>(randomTilePrefab.TileGameObject);
        tileInstance.Initialize(index);
        return tileInstance;
    }

    private ITile GetRandomTile(TileType tileType)
    {
        var prefabs = _tilePrefabs[tileType];
        return prefabs[Random.Range(0, prefabs.Count)];
    }
}
