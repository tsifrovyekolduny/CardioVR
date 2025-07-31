using System;
using UnityEngine;
using Zenject;

public class QuestTile : BaseTile, IQuestTile
{
    public override TileType tileType => TileType.Quest;
    [SerializeField] private Transform _questSpawnPoint;

    [Inject] protected IQuestManagmentSystem _questManager;
    public GameObject QuestPlace => _questSpawnPoint.gameObject;

}