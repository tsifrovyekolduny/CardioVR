using TMPro;
using UnityEngine;
using Zenject;

public class VisualComponentFactory
{
    IEffectService _effectService;

    [Inject]
    private void Construct(IEffectService effectService)
    {
        _effectService = effectService;
    }
    public IVisualComponent Create(GameObject go)
    {
        if (go.TryGetComponent(out CanvasGroup cg))
            return new CanvasVisual(cg);
        if (go.TryGetComponent(out UnityEngine.UI.Image img))
            return new ImageVisual(img);
        if (go.TryGetComponent(out SpriteRenderer sr))
            return new SpriteVisual(sr);
        if (go.TryGetComponent(out MeshRenderer mr))
            return new MeshVisual(mr, _effectService);
        if (go.TryGetComponent(out TMP_Text txt))
            return new TextVisual(txt);

        return null; // или кинуть исключение
    }
}