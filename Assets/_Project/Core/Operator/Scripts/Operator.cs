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
    
    public event Action OnSessionEnd;        
    public event Action<IQuest> OnQuestStarted;
    public event Action OnQuestEnd;
    public event Action<string> OnGettingAnswer;

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

    public void Tick()
    {
        if (_sessionIsEnabled) {
            _lostTime -= Time.deltaTime;

            if (_lostTime <= 0.1f)
            {
                EndSession();
            }
        }        
    }

    public void QuestStarted(IQuest quest)
    {
        OnQuestStarted.Invoke(quest);
    }
}
