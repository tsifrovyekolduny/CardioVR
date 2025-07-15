using UnityEngine;

public interface IQuestTile
{
    IQuest Quest { get; }
    GameObject QuestPlace { get; }
    void LoadQuest(IQuest quest);
}

