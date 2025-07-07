using System;
using UnityEngine;

public class OperatorPanel : IOperator
{
    public event Action OnProfileChose;
    public event Action OnSessionEnd;
    public event Action OnGivingHint;
    public event Action OnGettingAnswer;

    public void EndSession()
    {
        OnSessionEnd.Invoke();
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
