using System;
using UnityEngine;

public interface IOperator
{
    public event Action OnProfileChose;
    public event Action OnSessionEnd;
    public event Action<string> OnGivingHint;
    public event Action OnGettingAnswer;

    // ����� ������� ����� ������ ��������� � ������� �� � ������ ���������
    public event Action<string[]> OnQuestStarted;    
    public void QuestStarted(string[] hints);
}
