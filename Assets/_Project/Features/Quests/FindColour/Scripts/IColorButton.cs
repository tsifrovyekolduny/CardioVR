using JetBrains.Annotations;
using System;
using UnityEngine;

public interface IColorButton
{
    public Color TextColor { get; set; }
    public Color TargetColor { get; set; }
    public Action<Color> PressButton { get; set; }
    public bool IsClosed { get; }
    public void Hide();
    public void Select();
    public void SetActive(bool active);
    public void Init(Color textColor, Color targetColor);
}
