using System;
using UnityEngine;

public interface IQuest
{
    void Start();
    void Finish();
    bool IsCompleted();
    GameObject GameObject { get; }
    event Action OnCompleted;
}

public interface IQuestStateController
{
    QuestStates CurrentState { get; }
    void StartGame();
    void CompleteGame();
}

public interface IQuestVisualController
{
    void Show(bool instant = false);
    void Hide(bool instant = false);
    void SetParent(Transform parent);
    void SetLocalPosition(Vector3 position);
    void SetLocalRotation(Quaternion rotation);
}

public interface IQuestNarratorController
{
    void PlayGreeting();
    void PlayHint(string hint);
    void PlayCongrats();
    void PlayEnd();
}

public interface IQuestLogic
{
    bool IsCompleted();
}