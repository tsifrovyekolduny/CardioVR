using System;
using UnityEngine;
using Zenject;

public class QuestTile : BaseTile, IQuestTile
{
    public override TileType tileType => TileType.Quest;
    [SerializeField] private Transform _questSpawnPoint;
    [SerializeField] private VisibilityAnimator _animatorOfGate;

    [Inject] protected IQuestManagmentSystem _questManager;
    public GameObject QuestPlace => _questSpawnPoint.gameObject;        

    public void OpenGate()
    {
        _animatorOfGate.Hide();
    }
}