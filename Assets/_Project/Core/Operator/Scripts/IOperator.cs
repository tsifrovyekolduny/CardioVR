using System;
using UnityEngine;

public interface IOperator
{
    public event Action OnProfileChose;
    public event Action OnSessionEnd;
    public event Action<string> OnGivingHint;
    public event Action OnGettingAnswer;

    // Таким макаром квест узнает подсказки и раздает их в панели оператора
    public event Action<string[]> OnQuestStarted;    
    public void QuestStarted(string[] hints);
}
