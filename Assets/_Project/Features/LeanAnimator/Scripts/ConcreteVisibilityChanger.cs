using UnityEngine;


[RequireComponent(typeof(VisibilityAnimator))]
public class ConcreteVisibilityChanger : MonoBehaviour
{
    [SerializeField] protected float _duration = 3f;
    VisibilityAnimator _visibilityAnimator;
    
    void Awake()
    {
        _visibilityAnimator = GetComponent<VisibilityAnimator>();
    }    

    public void ChangeVisibility(string gameObjectName, bool isVisible)
    {
        _visibilityAnimator.SetVisibleConcrete(gameObjectName, isVisible, _duration);
    }
}
