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
        Debug.Log("Игра началась");
        foreach (var item in _visitors) {
            Debug.Log("Делаем активными");
            item.ChangeInteractiveStats(true);
        }
    }           
}
