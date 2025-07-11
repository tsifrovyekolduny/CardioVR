using UnityEngine;

public class CanvasVisual : IVisualComponent
{
    private readonly CanvasGroup _canvasGroup;

    public CanvasVisual(CanvasGroup canvasGroup) => _canvasGroup = canvasGroup;

    public void Show(float duration) =>
        LeanTween.alphaCanvas(_canvasGroup, 1f, duration);

    public void Hide(float duration) =>
        LeanTween.alphaCanvas(_canvasGroup, 0f, duration);
}