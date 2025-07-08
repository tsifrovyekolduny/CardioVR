using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField]
    private TMP_Text fioText;
    [SerializeField]
    private TMP_Text ageText;
    [SerializeField]
    private Button? button;
    private ChildProfile _profile;

    public void InitProfile(ChildProfile profile)
    {
        fioText.text = $"{profile.Surname} {profile.Name} {profile.Patronymic}";
        ageText.text = profile.Age.ToString();
        _profile = profile;
        if (button)
        {
            button.onClick.AddListener(OnClick.Invoke);
        }
    }

    public ChildProfile GetProfile()
    {
        return _profile;
    }
}
