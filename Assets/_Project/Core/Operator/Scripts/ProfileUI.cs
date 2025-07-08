using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fioText;
    [SerializeField]
    private TMP_Text ageText;
    [SerializeField]
    private Button button;

    public void InitProfile(string fio, string age)
    {
        fioText.text = fio;
        ageText.text = age;
    }
}
