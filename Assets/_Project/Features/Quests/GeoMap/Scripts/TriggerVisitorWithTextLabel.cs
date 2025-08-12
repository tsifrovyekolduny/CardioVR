using TMPro;
using UnityEngine;


public class TriggerVisitorWithTextLabel : TriggerVisitor
{
    [SerializeField] private TMP_Text _text;    

    protected override void Awake()
    {
        _text.text = _visitorName;
        base.Awake();
    }
}
