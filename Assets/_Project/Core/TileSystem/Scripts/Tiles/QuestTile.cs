using UnityEngine;
using Zenject;

public class QuestTile : BaseTile, IQuestTile
{
    public override TileType tileType => TileType.Quest;
    [SerializeField] private Transform _questSpawnPoint; // Точка спавна квеста на тайле

    private IQuestManager _questManager;
    private IQuest _currentQuest;

    public IQuest Quest => _currentQuest;
    public GameObject QuestPlace => _questSpawnPoint.gameObject;

    [Inject]
    private void Construct(IQuestManager questManager)
    {
        _questManager = questManager;
    }

    public override void Initialize(int index, int tileIndex)
    {
        base.Initialize(index, tileIndex);
        SpawnQuest();
    }


    private void SpawnQuest()
    {
        _currentQuest = _questManager.GetNextQuest();

        if (_currentQuest != null && _currentQuest is BaseQuest baseQuest)
        {
            baseQuest.OnQuestFinished += OnQuestCompleted;
            baseQuest.transform.SetParent(_questSpawnPoint);
            baseQuest.transform.localPosition = Vector3.zero;
            baseQuest.transform.localRotation = Quaternion.identity;
        }
    }


    private void OnQuestCompleted()
    {
        _tileManager.SpawnNextTile(TileType.Road);
    }

    public override void ExecuteTileBehavior()
    {
        base.ExecuteTileBehavior();

        if (_currentQuest != null && _currentQuest is BaseQuest baseQuest)
        {
            if (baseQuest.CurrentState == QuestStates.NotStarted)
            {
                baseQuest.StartGame();
            }
        }
    }


}