using UnityEngine;

public abstract class AbstractVisualWithColor : IVisualComponent
{
    protected Color _defaultColor;


    protected abstract GameObject GetGameObject();

    public void Hide(float duration)
    {
        _defaultColor = GetColor();
        LeanTween.value(GetGameObject(), UpdateColor, _defaultColor, Color.clear, duration);
    }

    public void Show(float duration)
    {
        LeanTween.value(GetGameObject(), UpdateColor, Color.clear, _defaultColor, duration);
    }

    protected abstract Color GetColor();
    protected abstract void UpdateColor(Color c);
}
