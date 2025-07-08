using System;
using System.Collections;
using UnityEngine;
using Zenject;

class Operator : IOperator, ITickable
{
    [SerializeField]
    // TODO время в зависимости от возраста ребенка
    public float _lostTime = 120f;
    public float LostTime { get { return _lostTime; } }    

    public event Action OnProfileChose;
    public event Action OnSessionEnd;
    public event Action OnGivingHint;
    public event Action OnGettingAnswer;

    private bool _sessionIsEnabled = false;

    public void StartSession()
    {
        _sessionIsEnabled = true;
        //_tickingTime = StartCoroutine(TickingTime());
    }

    public void EndSession()
    {
        _sessionIsEnabled = false;
        //StopCoroutine(_tickingTime);
        OnSessionEnd?.Invoke();
    }

    //IEnumerator TickingTime()
    //{
    //    _lostTime -= 1;
    //    if (_lostTime < 0)
    //    {
    //        EndSession();
    //    }
    //    yield return new WaitForSeconds(1);
    //}

    public void GiveHint()
    {
        OnGivingHint?.Invoke();
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
}
