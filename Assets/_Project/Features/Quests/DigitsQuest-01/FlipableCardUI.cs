using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlipableCardUI : MonoBehaviour
{
    private TMP_Text _text;
    private Button _button;
    [SerializeField]
    private float _animationTime = 3f;
    [SerializeField]
    private float _temporaryShowTime = 10f;
    private int _number;
    private bool _isOpened;
    public bool IsOpened { get { return _isOpened; } }

    public Action OnCardOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();
    }

    public void Flip(bool open, bool temporary = false)
    {
        _isOpened = open;

        float degree = open ? 0f : 180f;
        _button.interactable = !open;

        LTDescr animation = LeanTween.rotateLocal(gameObject, Vector3.up * degree, _animationTime);

        // Обычно temporary вызывается при открытии
        if (temporary)
        {
            Action action = () => Flip(!open);
            animation.setDelay(_animationTime * _temporaryShowTime).setOnComplete(action);
        }
        // Проверка числа после открытия карточки
        else
        {
            if (open)
            {
                animation.setDelay(_animationTime * 0.5f).setOnComplete(OnCardOpen);
            }
        }
    }

    public void ShowCard()
    {
        Flip(true);
    }

    public void SetNumber(int number)
    {
        _number = number;
        _text.text = _number.ToString();
    }
}
