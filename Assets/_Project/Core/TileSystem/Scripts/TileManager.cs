using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileManager : ITileManager, IInitializable, IDisposable
{
    private readonly DiContainer _container;
    private readonly TilePrefabsObject _tilePrefabsObject;

    private Dictionary<TileType, List<ITile>> _tilePrefabs = new Dictionary<TileType, List<ITile>>();
    private int _currentTileIndex;
    private ITile _currentTile;

    [Inject] private IQuestManager _questManager;
    [Inject] private IOperator _operator;
    [Inject]
    public TileManager(DiContainer container, TilePrefabsObject tilePrefabsObject)
    {
        _container = container;
        _tilePrefabsObject = tilePrefabsObject;
    }

    public void Initialize()
    {
        PreloadTiles();
        _operator.OnSessionEnd += HandleSessionEnd;
        SpawnNextTile(TileType.Road);
    }


    private void PreloadTiles()
    {
        _currentTileIndex = 0;
        foreach (var prefab in _tilePrefabsObject.TilePrefabs)
        {
            var tile = prefab.GetComponent<ITile>();

            if (tile == null)
            {
                continue;
            }

            var tileType = tile.tileType;
            if (!_tilePrefabs.ContainsKey(tileType))
            {
                _tilePrefabs[tileType] = new List<ITile>();
            }
            _tilePrefabs[tileType].Add(tile);
        }
    }

    public void SpawnNextTile(TileType tileType)
    {
        if (!_tilePrefabs.ContainsKey(tileType))
        {
            Debug.LogError($"Вы не добавили тайлов типа: {tileType}");
            return;
        }

        _currentTileIndex++;
        var randomTile = GetRandomTile(tileType);
        var tileInstance = _container.InstantiatePrefabForComponent<ITile>(randomTile.TileGameObject);
        tileInstance.Initialize(_currentTileIndex);
        tileInstance.IsActive = true;
        tileInstance.RequestNextTileAction += HandleTileSpawnRequest;
        _currentTile = tileInstance;
    }

    public void Dispose()
    {
        // TODO: Добавить функциональность очистки лишних ресурсов
    }

    public bool CheckNextTileExistence(int tileIndex)
    {
        return _currentTileIndex < tileIndex;
    }

    private ITile GetRandomTile(TileType tileType)
    {
        var prefabs = _tilePrefabs[tileType];
        return prefabs[UnityEngine.Random.Range(0, prefabs.Count)];
    }

    private void HandleSessionEnd()
    {
        switch (_questManager.AreAllQuestsCompleted())
        {
            case false:
                SpawnNextTile(TileType.Save);
                break;
        }

    }
    public void HandleTileSpawnRequest(ITile tile)
    {
        Debug.Log("Получил запрос, щас разберемся");
        if (CheckNextTileExistence(tile.TileIndex))
        {
            Debug.LogWarning("Внимание! Следующий тайл уже существует");
            return;
        }
        Debug.Log("Попытка спавна");
        switch (tile.tileType)
        {
            case TileType.Road:

                SpawnNextTile(TileType.Quest);
                _questManager.SpawnQuest(_currentTile);
                break;
        }
    }



}