using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OperatorView : MonoBehaviour, IOperatorView
{
    public event Action<string, string, string, int> ProfileSaveRequested;
    public event Action<string> SearchTextChanged;
    public event Action<ChildProfile> ProfileSelected;
    public event Action SessionStart;
    public event Action<string> HintGived;
    public event Action<int> AnswerGived;
    public event Action SessionEnd;

    [SerializeField] private TMP_InputField _surnameText;
    [SerializeField] private TMP_InputField _nameText;
    [SerializeField] private TMP_InputField _patronymicText;
    [SerializeField] private TMP_InputField _ageText;
    [SerializeField] private TMP_InputField _answerText;

    [SerializeField] private GameObject _profileBox;
    [SerializeField] private GameObject _hintBox;

    [SerializeField] private ProfileUI _profilePrefab;
    [SerializeField] private ProfileUI _chosenProfile;
    [SerializeField] private ProfileUI _chosedProfile;

    [SerializeField] private TMP_Text _timeLabel;
    [SerializeField] private HintUI _chosedHint;

    [SerializeField] private GameObject _page1;
    [SerializeField] private GameObject _page2;

    [SerializeField] private Button _saveProfile;
    [SerializeField] private Button _startSession;
    [SerializeField] private Button _endSession;
    [SerializeField] private Button _giveHint;
    [SerializeField] private Button _giveAnswer;
    [SerializeField] private Button _clearChosedHint;

    private OperatorPresenter _presenter;

    [Inject]
    void Construct(SaveSystem saveSystem, Operator @operator)
    {
        _presenter = new OperatorPresenter(saveSystem, @operator, this);
    }

    private void Start()
    {
        _presenter.Initialize();

        // Связь с полями ввода
        _surnameText.onValueChanged.AddListener((text) => SearchTextChanged?.Invoke(text));
        _nameText.onValueChanged.AddListener((text) => SearchTextChanged?.Invoke(text));
        _patronymicText.onValueChanged.AddListener((text) => SearchTextChanged?.Invoke(text));

        // Связь с кнопками
        _saveProfile.onClick.AddListener(OnProfileSaveClick);
        _startSession.onClick.AddListener(SessionStart.Invoke);
        _endSession.onClick.AddListener(SessionEnd.Invoke);
        _giveAnswer.onClick.AddListener(() => { AnswerGived.Invoke(int.Parse(_answerText.text)); });
        _giveHint.onClick.AddListener(() => { HintGived.Invoke(_chosedHint.GetHint()); });
        _endSession.onClick.AddListener(SessionEnd.Invoke);

        // Прочие кнопки
        _clearChosedHint.onClick.AddListener(ClearChosenHint);

        // Инициализация доступности и видимости
        _page1.SetActive(true);
        _page2.SetActive(false);

        _startSession.interactable = false;
        _giveHint.interactable = false;
        _giveAnswer.interactable = false;

        // Прячем запуленные объекты
        // TODO чтоб такого не было, лучше пул делать отдельно       
        foreach (Transform child in _hintBox.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void ClearChosenHint()
    {
        _chosedHint.Init("Подсказка не выбрана");
        _giveHint.interactable = false;
    }

    private void SetChosenHint(string text)
    {
        _chosedHint.Init(text);
        _giveHint.interactable = true;
    }

    private void Update()
    {
        _presenter.Update();
    }

    public void OnProfileSaveClick()
    {
        if (int.TryParse(_ageText.text, out int age))
        {
            ProfileSaveRequested?.Invoke(
                _surnameText.text,
                _nameText.text,
                _patronymicText.text,
                age);
            _startSession.interactable = true;
        }
    }

    public void ShowProfiles(List<ChildProfile> profiles)
    {
        for (int pooledProfileIndex = 0; pooledProfileIndex < _profileBox.transform.childCount; ++pooledProfileIndex)
        {
            ProfileUI profUI = _profileBox.transform.GetChild(pooledProfileIndex).GetComponent<ProfileUI>();
            if (pooledProfileIndex < profiles.Count)
            {
                ChildProfile prof = profiles[pooledProfileIndex];
                profUI.OnClick += () => { ShowChosenProfile(profUI.GetProfile()); };                
                profUI.Init(prof);
                profUI.gameObject.SetActive(true);
            }
            else
            {
                profUI.gameObject.SetActive(false);
            }
        }
    }

    public void ShowChosenProfile(ChildProfile profile)
    {
        _chosedProfile.Init(profile);
        _chosenProfile.Init(profile);
        _startSession.interactable = true;
    }

    public void UpdateTimer(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        _timeLabel.text = $"{minutes:00}:{seconds:00}";
    }

    public void ShowPage2()
    {
        _page1.SetActive(false);
        _page2.SetActive(true);
    }

    public void ClearInputFields()
    {
        _surnameText.text = "";
        _nameText.text = "";
        _patronymicText.text = "";
        _ageText.text = "";
    }

    // todo можно сделать в виде пула объектов, либо просто заранее запихать побольше подсказок
    public void ShowHints(string[] obj)
    {
        for (int hintElementIndex = 0; hintElementIndex < _hintBox.transform.childCount; hintElementIndex++)
        {
            HintUI hintText = _hintBox.transform.GetChild(hintElementIndex).GetComponent<HintUI>();
            if (hintElementIndex < obj.Length)
            {
                string hint = obj[hintElementIndex];
                hintText.OnClick += () => { SetChosenHint(hint); };
                hintText.Init(hint);
                hintText.gameObject.SetActive(true);
            }
            else
            {
                hintText.gameObject.SetActive(false);
            }
        }
    }
}
