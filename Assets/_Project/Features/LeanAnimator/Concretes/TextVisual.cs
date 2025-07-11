using TMPro;
using UnityEngine;

public class TextVisual : AbstractVisualWithColor
{
    private readonly TMP_Text _text;

    public TextVisual(TMP_Text text) => _text = text;

    protected override GameObject GetGameObject()
    {
        return _text.gameObject;
    }

    protected override Color GetColor()
    {
        return _text.color;
    }

    protected override void UpdateColor(Color c)
    {
        _text.color = c;
    }
}