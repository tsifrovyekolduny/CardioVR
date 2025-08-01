using FindColorExtension;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour, IColorButton
{
    private TMP_Text _text;
    private Button _button;

    [SerializeField]
    private VisibilityAnimator _visibilityAnimator;    
    public Color TextColor { get; set; }
    public Color TargetColor { get; set; }
    public Action<Color> PressButton { get; set; }

    // Проверка "спрятанности" по первому child, чтобы не проверять все childs
    public bool IsHidden => transform.GetChild(0).gameObject.activeSelf;

    void OnEnable()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { PressButton?.Invoke(TextColor); });

        _text.color = TextColor;
        _text.text = TargetColor.GetRuString();
    }

    public void Hide()
    {
        _visibilityAnimator.Hide();
    }
    
    void Awake()
    {
        _visibilityAnimator = GetComponent<VisibilityAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
