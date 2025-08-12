using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VisibilityAnimator : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField, Tooltip("Глубина поиска: 1 - только прямые дети")]
    private int _maxDepth = 1;

    [SerializeField]
    private List<VisualPair> _visualPairs = new List<VisualPair>();
    private VisualComponentFactory factory;

    [Inject]
    void Construct(VisualComponentFactory visualComponentFactory)
    {
        factory = visualComponentFactory;
    }

    private void Awake()
    {
        CollectVisualComponents(transform, 0);
    }

    // Рекурсивный сбор всех компонентов
    private void CollectVisualComponents(Transform currentTransform, int currentDepth)
    {
        if (currentDepth >= _maxDepth) return;

        foreach (Transform child in currentTransform)
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

            CollectVisualComponents(child, currentDepth + 1); // рекурсия
        }
    }

    public void Show(bool instant = false)
    {
        float fadeDuration = instant ? 0f : _fadeDuration;

        foreach (VisualPair visualPair in _visualPairs)
        {
            visualPair.GameObject.SetActive(true);
            visualPair.Component?.Show(fadeDuration);
        }
    }

    public void Hide(bool instant = false)
    {
        float fadeDuration = instant ? 0f : _fadeDuration;        

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