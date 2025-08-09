using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestPrefabsObject", menuName = "Game/Quest Settings")]
public class QuestPrefabsObject : ScriptableObject
{
    public BaseQuest[] QuestPrefabs;
}