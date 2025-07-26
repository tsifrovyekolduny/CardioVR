using System;
using UnityEngine;
using Zenject;

public class QuestTile : BaseTile, IQuestTile
{
    public override TileType tileType => TileType.Quest;
    [SerializeField] private Transform _questSpawnPoint; // Точка спавна квеста на тайле

    [Inject] protected IQuestManager _questManager;
    private IQuest _currentQuest;

    public IQuest Quest => _currentQuest;
    public GameObject QuestPlace => _questSpawnPoint.gameObject;

    public override void Initialize(int index)
    {
        base.Initialize(index);
    }


}