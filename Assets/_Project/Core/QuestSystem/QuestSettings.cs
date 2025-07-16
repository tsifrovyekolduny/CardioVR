using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSettings", menuName = "Game/Quest Settings")]
public class QuestSettings : ScriptableObject
{
    [SerializeField] private List<BaseQuest> _questPrefabs;
    public IReadOnlyList<BaseQuest> QuestPrefabs => _questPrefabs;
}