using System;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityAnimator : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;

    [SerializeField]
    private List<VisualPair> _visualPairs = new List<VisualPair>();
    private VisualComponentFactory factory = new();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            var visualComponent = factory.Create(child.gameObject);
            if (visualComponent != null)
            {
                _visualPairs.Add(new VisualPair
                {
                    Component = visualComponent,
                    GameObject = child.gameObject
                });
            }
        }
    }

    public void Show()
    {        
        foreach (VisualPair visualPair in _visualPairs)
        {
            visualPair.GameObject.SetActive(true);
            visualPair.Component?.Show(fadeDuration);
        }
        
    }

    public void Hide()
    {
        foreach (VisualPair visualPair in _visualPairs)
        {
            visualPair.Component?.Hide(fadeDuration);            
            LeanTween.delayedCall(fadeDuration, () => visualPair.GameObject.SetActive(false));
        }        
    }
}

[Serializable]
public struct VisualPair
{
    public IVisualComponent Component;
    public GameObject GameObject;
}