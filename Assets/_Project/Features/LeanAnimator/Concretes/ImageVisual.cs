using System;
using TMPro;
using UnityEngine;

public class ImageVisual : AbstractVisualWithColor
{
    private readonly UnityEngine.UI.Image _image;        

    public ImageVisual(UnityEngine.UI.Image image) => _image = image;
    

    protected override Color GetColor()
    {
        return _image.color;
    }

    protected override void UpdateColor(Color c)
    {
        _image.color = c;
    }

    protected override GameObject GetGameObject()
    {
        return _image.gameObject;
    }
}