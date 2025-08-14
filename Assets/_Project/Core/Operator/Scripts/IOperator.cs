using System;
using UnityEngine;

public interface IOperator
{
    float LostTime { get; }

    public event Action OnSessionEnd;    
    public event Action<string> OnGettingAnswer;    
    public event Action<IQuest> OnQuestStarted;
    public event Action OnQuestEnd;

    void EndSession();
    void QuestStarted(IQuest quest);
    void QuestEnded();
    
    void StartSession();
}
