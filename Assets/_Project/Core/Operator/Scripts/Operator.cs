using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Operator : IOperator, ITickable
{
    [SerializeField]
    // TODO время в зависимости от возраста ребенка
    public float _lostTime = 60f * 15;
    public float LostTime { get { return _lostTime; } }    
    
    public event Action OnSessionEnd;        
    public event Action<IQuest> OnQuestStarted;
    public event Action OnQuestEnd;
    public event Action<string> OnGettingAnswer;

    private bool _sessionTimerIsEnabled = false;

    public void StartSession()
    {        
        _sessionTimerIsEnabled = true;        
    }

    public void EndSession()
    {
        _sessionTimerIsEnabled = false;     
        OnSessionEnd?.Invoke();
    }

    public void Tick()
    {
        if (_sessionTimerIsEnabled) {
            _lostTime -= Time.deltaTime;

            if (_lostTime <= 0.1f)
            {
                _sessionTimerIsEnabled = false;
            }
        }        
    }

    public void QuestEnded()
    {
        OnQuestEnd?.Invoke();
    }

    public void QuestStarted(IQuest quest)
    {
        OnQuestStarted?.Invoke(quest);
    }
}
