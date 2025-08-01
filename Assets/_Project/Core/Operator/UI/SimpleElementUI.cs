using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SimpleElementUI : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField]
    protected TMP_Text text1;
    [SerializeField]
    private Button? button;    

    protected void InitButton()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnClick.Invoke);
        }
    }

    abstract public void Init(object container);
}
