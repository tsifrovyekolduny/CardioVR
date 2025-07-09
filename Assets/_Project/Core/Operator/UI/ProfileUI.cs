using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : SimpleElementUI
{        
    [SerializeField]
    private TMP_Text text2;
    [SerializeField]    
    private ChildProfile _profile;    

    public ChildProfile GetProfile()
    {
        return _profile;
    }

    public override void Init(object container)
    {
        ChildProfile profile = container as ChildProfile;
        text1.text = $"{profile.Surname} {profile.Name} {profile.Patronymic}";
        text2.text = profile.Age.ToString();
        _profile = profile;
        InitButton();
    }
}
