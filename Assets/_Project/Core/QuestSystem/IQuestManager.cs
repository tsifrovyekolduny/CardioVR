using System.Collections.Generic;

public interface IQuestManager
{
    void ResetQuests();
    IQuest ChangeQuestToNext();
    bool AreAllQuestsCompleted();

}
