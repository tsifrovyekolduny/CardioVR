using System.Collections.Generic;

public interface IQuestManager
{
    IQuest GetNextQuest();
    void ResetQuests();
    IQuest GetCurrentQuest();
    bool AreAllQuestsCompleted();

}
