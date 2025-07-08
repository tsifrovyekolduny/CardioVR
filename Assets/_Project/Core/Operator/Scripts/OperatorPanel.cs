using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Линковка кнопок, переходы

class OperatorPanel : MonoBehaviour
{
    private SaveSystem _saveSystem;
    private Operator _operator;
    private List<ChildProfile> _profiles;


    [Header("Pages")]
    [SerializeField] private GameObject _page1;
    [SerializeField] private GameObject _page2;

    [Header("ChooseProfile")]
    [SerializeField] private TMP_Text _surnameText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _patronymicText;
    [SerializeField] private TMP_Text _ageText;
    [SerializeField] private TMP_Text _chosenFioText;
    [SerializeField] private TMP_Text _chosenAgeText;
    [SerializeField] private GameObject _profileBox;

    [Header("Session")]
    [SerializeField] private TMP_Text _timeLabel;
    [SerializeField] private TMP_Text _chosedFioText;
    [SerializeField] private TMP_Text _chosedAgeText;
    [SerializeField] private GameObject hintBox;

    [Header("Prefabs")]
    [SerializeField] private GameObject _hintPrefab;
    [SerializeField] private ProfileUI _profilePrefab;

    private void Start()
    {
        // Активация страниц
        _page1.SetActive(true);
        _page2.SetActive(false);

        // Активация дисплея
        if (Display.displays.Length > 1)
        {
            Debug.Log("Display2 activated");
            Display.displays[1].Activate();
        }

        // Прогрузка профилей
        SyncProfiles();
    }

    private void SyncProfiles()
    {        
         = _saveSystem.GetProfiles();                

        for (int pooledProfileIndex = 0; pooledProfileIndex < _profileBox.transform.childCount; ++pooledProfileIndex)
        {
            ProfileUI profUI = _profileBox.transform.GetChild(pooledProfileIndex).GetComponent<ProfileUI>();
            if (pooledProfileIndex < profiles.Count) {
                ChildProfile prof = profiles[pooledProfileIndex];
                profUI.InitProfile($"{prof.Surname} {prof.Name} {prof.Patronymic}", prof.Age.ToString());
            }
            else
            {
                profUI.gameObject.SetActive(false);
            }   

        }
    }

    public void OnFioChanged(string text)
    {
        
    }

    private void FixedUpdate()
    {
        SyncTimeAndLabel();
    }

    [Inject]
    private void Construct(SaveSystem saveSystem, Operator @operator)
    {
        _saveSystem = saveSystem;
        _operator = @operator;
    }

    public void ShowPage2()
    {
        _page1.SetActive(false);
        _page2.SetActive(true);
        _operator.StartSession();
    }

    public void EndSession()
    {
        _operator.EndSession();

    }

    public void SyncTimeAndLabel()
    {
        var minutes = math.floor(_operator.LostTime / 60);
        var seconds = math.floor(_operator.LostTime % 60);
        _timeLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnFIOChanged(TMP_Text fioText)
    {
        string fio = fioText.text;

        // Todo выполнить поиск
    }

    public void OnProfileClick(GameObject sender)
    {
        TMP_Text[] texts = sender.GetComponentsInChildren<TMP_Text>();
        setChosenProfile(texts[0].text, texts[1].text);
    }

    public void OnProfileSaveClick()
    {
        string surname = _surnameText.text;
        string name = _nameText.text;
        string patronymic = _patronymicText.text;
        string age = _ageText.text;

        _saveSystem.SaveProfile(surname, name, patronymic, age);

        setChosenProfile($"{surname} {name} {patronymic}", age);
    }    

    public void setChosenProfile(string fio, string age)
    {
        _chosenAgeText.text = fio;
        _chosenFioText.text = age;
        _chosedFioText.text = fio;
        _chosedAgeText.text = age;
    }
}

