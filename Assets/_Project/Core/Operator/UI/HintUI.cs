using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : SimpleElementUI
{     
    public override void Init(object container)
    {
        string hint = container as string;
        text1.text = hint;    
    
        InitButton();
    }

    public string GetHint()
    {
        return text1.text;
    }
}
