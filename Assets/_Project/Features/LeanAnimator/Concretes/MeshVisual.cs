using UnityEngine;

public class MeshVisual : AbstractVisualWithColor
{
    private readonly MeshRenderer _renderer;

    public MeshVisual(MeshRenderer renderer) => _renderer = renderer;    

    protected override GameObject GetGameObject()
    {
        return _renderer.gameObject;
    }

    protected override Color GetColor()
    {
        return _renderer.material.color;
    }

    protected override void UpdateColor(Color c)
    {
        _renderer.material.color = c;
    }
}