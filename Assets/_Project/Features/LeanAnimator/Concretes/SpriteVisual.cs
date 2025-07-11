using UnityEngine;

public class SpriteVisual : AbstractVisualWithColor
{
    private readonly SpriteRenderer _renderer;

    public SpriteVisual(SpriteRenderer renderer) => _renderer = renderer;

    protected override GameObject GetGameObject()
    {
        return _renderer.gameObject;
    }

    protected override Color GetColor()
    {
        return _renderer.color;
    }

    protected override void UpdateColor(Color c)
    {
        _renderer.color = c;
    }
}