using System;
using System.Collections;
using UnityEngine;

abstract class Operator : MonoBehaviour, IOperator
{
    [SerializeField]
    // TODO время в зависимости от возраста ребенка
    public long _lostTime = 20 * 60;
    public long LostTime { get { return _lostTime; } }
    private Coroutine _tickingTime;

    public event Action OnProfileChose;
    public event Action OnSessionEnd;
    public event Action OnGivingHint;
    public event Action OnGettingAnswer;

    public void StartSession()
    {
        _tickingTime = StartCoroutine(TickingTime());
    }

    public void EndSession()
    {
        StopCoroutine(_tickingTime);
        OnSessionEnd.Invoke();
    }

    IEnumerator TickingTime()
    {
        _lostTime -= 1;
        if (_lostTime < 0)
        {
            EndSession();
        }
        yield return new WaitForSeconds(1);
    }

    public void GiveHint()
    {
        OnGivingHint.Invoke();
    }

    public void GiveAnswer()
    {
        OnGettingAnswer.Invoke();
    }
    
}
