using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Operator : IOperator, ITickable
{
    [SerializeField]
    // TODO время в зависимости от возраста ребенка
    public float _lostTime = 120f;
    public float LostTime { get { return _lostTime; } }    

    public event Action OnProfileChose;
    public event Action OnSessionEnd;
    public event Action<string> OnGivingHint;
    public event Action OnGettingAnswer;
    public event Action<string[]> OnQuestStarted;
    public event Action OnQuestEnd;

    private bool _sessionIsEnabled = false;

    public void StartSession()
    {        
        _sessionIsEnabled = true;        
    }

    public void EndSession()
    {
        _sessionIsEnabled = false;     
        OnSessionEnd?.Invoke();
    }    
    public void GiveHint(string hint)
    {
        OnGivingHint?.Invoke(hint);
    }

    public void GiveAnswer()
    {
        OnGettingAnswer?.Invoke();
    }

    public void Tick()
    {
        if (_sessionIsEnabled) {
            _lostTime -= Time.deltaTime;

            if (_lostTime <= 0.0f)
            {
                EndSession();
            }
        }        
    }

    public void QuestStarted(string[] hints)
    {
        OnQuestStarted.Invoke(hints);
    }
}
