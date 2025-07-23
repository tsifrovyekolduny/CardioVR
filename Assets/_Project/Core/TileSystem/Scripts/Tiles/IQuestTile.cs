using System;
using UnityEngine;

public interface IQuestTile
{
    IQuest Quest { get; }
    GameObject QuestPlace { get; }
    event Action<ITile> RequestNextQuestSpawnAction;
    void RequestNextQuestSpawn(ITile requestingTile);
}

