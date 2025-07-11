using TMPro;
using UnityEngine;

public class VisualComponentFactory
{
    public IVisualComponent Create(GameObject go)
    {
        if (go.TryGetComponent(out CanvasGroup cg))
            return new CanvasVisual(cg);
        if (go.TryGetComponent(out UnityEngine.UI.Image img))
            return new ImageVisual(img);
        if (go.TryGetComponent(out SpriteRenderer sr))
            return new SpriteVisual(sr);
        if (go.TryGetComponent(out MeshRenderer mr))
            return new MeshVisual(mr);
        if (go.TryGetComponent(out TMP_Text txt))
            return new TextVisual(txt);

        return null; // или кинуть исключение
    }
}