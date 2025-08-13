using UnityEngine;

public abstract class AbstractVisualWithColor : IVisualComponent
{
    protected Color _defaultColor;
    private bool _coloredOnce = false;


    protected abstract GameObject GetGameObject();

    public void Hide(float duration)
    {
        if (!_coloredOnce) {
            Debug.Log($"Setting {GetColor()} to  VC");
            _defaultColor = GetColor();
            _coloredOnce = true;
        }        
        LeanTween.value(GetGameObject(), UpdateColor, _defaultColor, Color.clear, duration);
    }

    public void Show(float duration)
    {
        Debug.Log($"DefaultColor of VC is {_defaultColor}");
        LeanTween.value(GetGameObject(), UpdateColor, Color.clear, _defaultColor, duration);
    }

    protected abstract Color GetColor();
    protected abstract void UpdateColor(Color c);
}
