using System.Linq;
using UnityEngine;

public class GeoMapLogic : MonoBehaviour, IQuestLogic
{
    [SerializeField] private MagneticTriggerWaiter[] _triggerWaiters;
    public bool IsCompleted()
    {
        return _triggerWaiters.All(g => g.IsRightEntrance);
    }

    public void StartLogic()
    {
        
    }    
}
