using FindColorExtension;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour, IColorButton
{
    private TMP_Text _text;
    private Button _button;
    private bool _isClosed;

    [SerializeField]
    private VisibilityAnimator _visibilityAnimator;
    public Color TextColor { get; set; }
    public Color TargetColor { get; set; }
    public Action<Color> PressButton { get; set; }

    // Проверка "спрятанности" по первому child, чтобы не проверять все childs
    public bool IsClosed { get { return _isClosed; } }

    void Awake()
    {
        _visibilityAnimator = GetComponent<VisibilityAnimator>();

        _text = GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { PressButton?.Invoke(TextColor); });
    }

    public void Hide()
    {
        _visibilityAnimator.Hide();
        _isClosed = true;
    }

    public void Init(Color textColor, Color targetColor)
    {
        TextColor = textColor;
        TargetColor = targetColor;

        _text.color = TextColor;
        _text.text = TargetColor.GetRuString();
    }

    public void Select()
    {
        _button.Select();
    }

    public void SetActive(bool active)
    {
        _button.interactable = active;
    }
}
