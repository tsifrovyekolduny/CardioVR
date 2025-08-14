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
    public event Action SessionEnd;
    public event Action<string> AnswerGived;
    #region == UI поля ==
    [Header("Создать профиль")]
    [SerializeField] private TMP_InputField _surnameText;
    [SerializeField] private TMP_InputField _nameText;
    [SerializeField] private TMP_InputField _patronymicText;
    [SerializeField] private TMP_InputField _ageText;
    [SerializeField] private Button _saveProfile;

    [Header("Сохранить ответ игрока")]
    [SerializeField] private TMP_InputField _answerText;
    [SerializeField] private Button _giveAnswer;

    [Header("Выбор профиля")]
    [SerializeField] private GameObject _profileBox;
    [SerializeField] private ProfileUI _profilePrefab;
    [SerializeField] private ProfileUI _chosenProfile;
    [SerializeField] private ProfileUI _chosedProfile;

    [Header("Таймер")]
    [SerializeField] private TMP_Text _timeLabel;

    [Header("Страницы")]
    [SerializeField] private GameObject _page1;
    [SerializeField] private GameObject _page2;

    [Header("Кнопки управления сессией")]
    [SerializeField] private Button _startSession;
    [SerializeField] private Button _endSession;

    [Header("Фазы квеста")]
    [SerializeField] private Button _nextPhaseButton;
    [SerializeField] private TMP_Text _phaseDescription;
    [SerializeField] private TMP_Text _phaseName;
    [SerializeField] private TMP_Text _phaseCount;

    #endregion
    private OperatorPresenter _presenter;
    private List<Phase> _phases;
    private int _currentPhaseIndex;

    [Inject]
    void Construct(SaveSystem saveSystem, IOperator @operator)
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
        InitPhaseButtons();

        // Инициализация доступности и видимости
        _page1.SetActive(true);
        _page2.SetActive(false);

        _startSession.interactable = false;
        _giveAnswer.interactable = false;
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
                profUI.gameObject.SetActive(true);
                ChildProfile prof = profiles[pooledProfileIndex];
                profUI.OnClick += () => { ProfileSelected.Invoke(prof); };
                profUI.Init(prof);
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

    public void SetPhases(List<Phase> phases)
    {
        _currentPhaseIndex = 0;
        _phases = phases;
        if (phases.Count > 0) { 
            SetNextPhase();
        }
    }

    #region == Работа с фазами квеста ==
    private void InitPhaseButtons()
    {
        _nextPhaseButton.onClick.AddListener(SetNextPhase);
    }

    private void SetNextPhase()
    {
        if(_currentPhaseIndex < _phases.Count)
        {
            _nextPhaseButton.GetComponent<TMP_Text>().text = "Начать фазу";
            var phase = _phases[_currentPhaseIndex];
            phase.SomeAction.Invoke();
            _phaseDescription.text = phase.Description;
            _phaseName.text = $"Следующая фаза: {phase.Name}";

            ++_currentPhaseIndex;
        }
        // фаз больше не осталось
        else
        {
            ClearPhaseFields();
            _nextPhaseButton.GetComponent<TMP_Text>().text = "Завершить игру";
        }
    }

    private void ClearPhaseFields()
    {
        _phaseDescription.text = "Описание фазы";
        _phaseName.text = "Название фазы";
    }

    #endregion
}
