using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
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
    [SerializeField] private TMP_InputField _surnameText;
    [SerializeField] private TMP_InputField _nameText;
    [SerializeField] private TMP_InputField _patronymicText;
    [SerializeField] private TMP_InputField _ageText;
    [SerializeField] private ProfileUI _chosenProfile;
    [SerializeField] private GameObject _profileBox;

    [Header("Session")]
    [SerializeField] private TMP_Text _timeLabel;
    [SerializeField] private ProfileUI _chosedProfile;
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

    [Inject]
    private void Construct(SaveSystem saveSystem, Operator @operator)
    {
        _saveSystem = saveSystem;
        _operator = @operator;
    }

    private void SyncProfiles(string searchText = "")
    {
        List<ChildProfile> profiles;
        if (searchText == "")
        {
            _profiles = _saveSystem.GetProfiles();
            profiles = _profiles;
        }
        else
        {
            profiles = _profiles.Where(c => c.Patronymic.Contains(searchText) ||
                c.Name.Contains(searchText) ||
                c.Surname.Contains(searchText)).ToList();
        }

        for (int pooledProfileIndex = 0; pooledProfileIndex < _profileBox.transform.childCount; ++pooledProfileIndex)
        {
            ProfileUI profUI = _profileBox.transform.GetChild(pooledProfileIndex).GetComponent<ProfileUI>();
            if (pooledProfileIndex < profiles.Count)
            {
                ChildProfile prof = profiles[pooledProfileIndex];                
                profUI.OnClick += () => { OnProfileClick(profUI.GetProfile()); };
                profUI.InitProfile(prof);
                profUI.gameObject.SetActive(true);
            }
            else
            {
                profUI.gameObject.SetActive(false);
            }

        }
    }

    public void OnFioChanged(TMP_Text text)
    {
        SyncProfiles(text.text);
    }

    private void FixedUpdate()
    {
        SyncTimeAndLabel();
    }

    public void ShowPage2()
    {
        _page1.SetActive(false);
        _page2.SetActive(true);
        _operator.StartSession();

        _saveSystem.SetProfile(_chosenProfile.GetProfile());        
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

    public void OnProfileClick(ChildProfile profile)
    {        
        setChosenProfile(profile);
    }

    public void OnProfileSaveClick()
    {
        string surname = _surnameText.text;
        string name = _nameText.text;
        string patronymic = _patronymicText.text;        
        string age = _ageText.text.ToString();

        try
        {
            var profile = _saveSystem.SaveProfile(surname, name, patronymic, Convert.ToInt32(age));
            setChosenProfile(profile);
            SyncProfiles();
        }
        catch(Exception e)
        {
            Debug.LogWarning(e.Message);
        }
        
    }

    public void setChosenProfile(ChildProfile profile)
    {
        _chosedProfile.InitProfile(profile);
        _chosenProfile.InitProfile(profile);
    }
}

