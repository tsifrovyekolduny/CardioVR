using System.Linq;
using UnityEngine;

public class GeoMapLogic : MonoBehaviour, IQuestLogic
{
    [SerializeField] private MagneticTriggerWaiter[] _triggerWaiters;
    [SerializeField] private TriggerVisitor[] _triggerVisitors;
    public bool IsCompleted()
    {
        return _triggerWaiters.All(g => g.IsRightEntrance);
    }

    public void StartLogic()
    {
        SetVisibleToVisitors(false);
    }

    public void SetVisibleToVisitors(bool visible)
    {
        foreach (var trigger in _triggerVisitors)
        {
            trigger.gameObject.SetActive(visible);
        }
    }

    public void HideWaiters()
    {
        foreach (var waiter in _triggerWaiters)
        {
            var textHider = waiter.GetComponent<TextVisibilityChanger>();
            textHider.Hide();
        }
    }
}
