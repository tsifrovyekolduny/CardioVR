using System;
using UnityEngine;

public class QuestStateController : MonoBehaviour, IQuestStateController
{
    public QuestStates CurrentState { get; private set; } = QuestStates.NotStarted;
    public event Action OnCompleted;
    public event Action OnStarted;

    public void StartGame()
    {
        CurrentState = QuestStates.Started;
        OnStarted?.Invoke();      
    }

    public void CompleteGame()
    {
        CurrentState = QuestStates.Finished;
        OnCompleted?.Invoke();
    }
}