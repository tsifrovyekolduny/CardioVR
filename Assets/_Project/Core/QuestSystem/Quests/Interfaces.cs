using System;
using System.Collections.Generic;
using UnityEngine;

public interface IQuest
{       
    GameObject GameObject { get; }    
    T GetQuestController<T>() where T : class, IQuestController;
}

public interface IQuestController { };

public interface IQuestStateController : IQuestController
{
    QuestStates CurrentState { get; }
    void StartGame();
    void CompleteGame();
    event Action OnStarted;
    event Action OnCompleted;
}

public interface IQuestVisualController : IQuestController
{
    void Show(bool instant = false, bool firstTime = false);
    void Hide(bool instant = false, bool firstTime = false);
    void SetParent(Transform parent);
    void SetLocalPosition(Vector3 position);
    void SetLocalRotation(Quaternion rotation);
}

public interface IQuestNarratorController : IQuestController
{
    void PlayGreeting();
    void PlayHint(string hint);
    void PlayCongrats();
    void PlayEnd();
}

public interface IQuestLogic : IQuestController
{
    bool IsCompleted();
    void StartLogic();
}
public interface IQuestPhasable : IQuestController
{
    List<Phase> Phases { get; }
    void Initialize(List<Phase> phases);
    void NextPhase();
    bool IsPhasesGone();
}