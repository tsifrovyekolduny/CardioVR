using System;
using UnityEngine;

public interface IOperator
{
    float LostTime { get; }

    public event Action OnSessionEnd;    
    public event Action<string> OnGettingAnswer;    
    public event Action<IQuest> OnQuestStarted;

    void EndSession();
    public void QuestStarted(IQuest quest);
    void StartSession();
}
