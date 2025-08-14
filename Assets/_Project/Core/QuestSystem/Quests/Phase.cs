using UnityEngine.Events;

[System.Serializable]
public class Phase
{
    private bool _isGone;

    public string Name;
    public string Description;
    public UnityAction SomeAction;
    public bool IsGone => _isGone;

    public void Complete()
    {
        _isGone = true;
    }

    public Phase(string name, string description, UnityAction action)
    {
        Name = name;
        Description = description;
        SomeAction = action;
    }
}