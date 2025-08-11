using System.Linq;
using UnityEngine;

public class AnimalsHouseQuest : MonoBehaviour, IQuestLogic
{
    [SerializeField] private TriggerWaiter[] _waiters;
    [SerializeField] private TriggerVisitor[] _visitors;
    public bool IsCompleted()
    {
        return _waiters.All(g => g.IsRightEntrance);
    }

    public void StartLogic()
    {
        foreach (var item in _visitors) { 
            item.ChangeInteractiveStats(true);
        }
    }           
}
