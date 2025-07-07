using System;
using UnityEngine;

public interface IOperator
{
    public event Action OnProfileChose;
    public event Action OnSessionEnd;
    public event Action OnGivingHint;
    public event Action OnGettingAnswer;
}
