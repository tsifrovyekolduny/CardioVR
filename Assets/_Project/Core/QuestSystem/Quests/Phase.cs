using System;
using UnityEngine.Events;

[System.Serializable]
public class Phase
{
    private bool _isGone;

    public string Name;
    public string Description;    
    public UnityEvent SomeAction;
    public bool IsGone => _isGone;

    public void Complete()
    {
        SomeAction?.Invoke();
        _isGone = true;
    }

    public Phase(string name, string description, UnityEvent action)
    {
        Name = name;
        Description = description;
        SomeAction = action;
    }
}