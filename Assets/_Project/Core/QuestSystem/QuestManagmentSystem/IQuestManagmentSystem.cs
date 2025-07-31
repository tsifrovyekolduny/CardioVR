using System.Collections.Generic;

public interface IQuestManagmentSystem
{
    bool AreAllQuestsCompleted();

    void SpawnQuest(ITile tile);
}
