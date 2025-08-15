using System.Linq;
using UnityEngine;

public class GeoMapLogic : MonoBehaviour, IQuestLogic
{
    [SerializeField] private MagneticTriggerWaiter[] _triggerWaiters;
    [SerializeField] private VisibilityAnimator[] _triggerVisitorsVisChangers;
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
        foreach (var triggerVisChanger in _triggerVisitorsVisChangers)
        {
            if (visible)
            {
                triggerVisChanger.gameObject.SetActive(true);
                triggerVisChanger.Show();
            }
            else
            {                
                triggerVisChanger.Hide(instant: true);
                triggerVisChanger.gameObject.SetActive(false);
            }
            
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
