using UnityEngine;
using Zenject;

public class TileManagmentSystem : ITileManagmentSystem, IInitializable
{
    private int _currentTileIndex;
    private ITile _currentTile;
    private bool _isSessionEnded = false;

    [Inject] private readonly IQuestManagmentSystem _questManager;
    [Inject] private readonly ITileFactory _tileFactory;
    [Inject] private readonly IOperator _operator;

    public void Initialize()
    {
        _operator.OnSessionEnd += HandleSessionEnd;
        SessionStart();
    }

    public void SessionStart() //TODO: �������� ����� ��������� ������ � ����������� �� ��������� ������
    {
        SpawnNextTile(TileType.Start);
        SpawnNextTile(TileType.Road);
    }

    public void SpawnNextTile(TileType tileType)
    {
        ITile tileInstance = _tileFactory.CreateTile(tileType, _currentTileIndex);
        tileInstance.RequestNextTileAction += HandleTileSpawnRequest;
        _currentTile = tileInstance;
        ++_currentTileIndex;
    }

    public bool CheckNextTileExistence(int tileIndex)
    {
        return _currentTileIndex < tileIndex;
    }

    private void HandleSessionEnd()
    {
        _isSessionEnded = true;
        if (!_questManager.AreAllQuestsCompleted())
        {
            SpawnNextTile(TileType.Save);
            return;
        }
    }

    public void HandleTileSpawnRequest(ITile tile) //TODO: � ������� �������� ������ ��� �� ������� ���������, ����� �������� ����������
    {
        Debug.Log("Tiler ������� ������ �� �����");
        if (_isSessionEnded)
        {
            Debug.LogWarning("����� ��������. ����� ��������");
            return;
        }
        if (CheckNextTileExistence(tile.TileIndex))
        {
            Debug.LogWarning("��������! ��������� ���� ��� ����������");
            return;
        }
        Debug.Log("�������� �����!");
        switch (tile.tileType)
        {
            case TileType.Road:
                SpawnNextTile(TileType.Quest);
                _questManager.SpawnQuest(_currentTile);
                break;
            case TileType.Quest:
                SpawnNextTile(TileType.Road);
                break;
            case TileType.Save:
                SpawnNextTile(TileType.Road);
                break;
            case TileType.Start:
                SpawnNextTile(TileType.Road);
                break;
                //TODO: �������� ������ ���������������� ��� ��������� ������� �� ������ ����� Save � Start
        }
        tile.RequestNextTileAction -= HandleTileSpawnRequest;
    }
}