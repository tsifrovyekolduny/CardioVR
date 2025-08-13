using TMPro;
using UnityEngine;


public class TextVisibilityChanger : ConcreteVisibilityChanger
{
    [SerializeField] private TMP_Text text;
    public void Start()
    {
        // TODO Надеется на имя GameObject. Не есть хорошо
        text.text = gameObject.name;
    }

    public void Hide()
    {
        ChangeVisibility(text.gameObject.name, false);
    }
}