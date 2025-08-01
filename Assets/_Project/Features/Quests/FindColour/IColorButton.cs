using System;
using UnityEngine;

public interface IColorButton
{
    public Color TextColor { get; set; }
    public Color TargetColor { get; set; }
    public Action<Color> PressButton { get; set; }
    public bool IsHidden { get; }
    public void Hide();
}
