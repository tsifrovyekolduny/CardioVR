using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// TODO Класс разросся! Нужен рефакторинг!
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

    [Header("Шкала для оценки действий игрока")]
    [SerializeField] private TMP_Text _criterionText;
    [SerializeField] private Slider _criterionValue;
    [SerializeField] private TMP_Text _warningCriteriaText;

    [Header("Фазы квеста")]
    [SerializeField] private Button _nextPhaseButton;
    [SerializeField] private TMP_Text _phaseDescription;
    [SerializeField] private TMP_Text _phaseName;
    [SerializeField] private TMP_Text _phaseCount;
    [SerializeField] private GameObject _phaseBox;

    #endregion
    private OperatorPresenter _presenter;
    private List<Phase> _phases;
    private string _currentQuest;
    private string _currentCriterion;
    private int _currentPhaseIndex;    

    [Inject]
    void Construct(ISaveSystem saveSystem, IOperator @operator)
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
        ClearPhaseFields();
        _criterionValue.onValueChanged.AddListener(CriteriaChange);

        _startSession.interactable = false;        
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

    private void CriteriaChange(float value)
    {
        if(value < 1f)
        {
            _warningCriteriaText.gameObject.SetActive(true);
        }
        else
        {
            _warningCriteriaText.gameObject.SetActive(false);
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
        if (phases.Count > 0)
        {
            _phaseBox.gameObject.SetActive(true);
            InitPhase(_phases[0]);
        }
        else
        {
            _phaseBox.gameObject.SetActive(false);
        }
    }

    public void SetVisibleQuestUI(bool visible)
    {
        _phaseBox.SetActive(visible);
        _giveAnswer.gameObject.SetActive(visible);
        _answerText.gameObject.SetActive(visible);
        _criterionText.gameObject.SetActive(visible);
        _warningCriteriaText.gameObject.SetActive(visible);
        _criterionValue.gameObject.SetActive(visible);
    }

    public void SetVisibleQuestUI(bool visible, IQuest quest)
    {
        if (quest.IsDesignedForPlayerAnswers)
        {
            _giveAnswer.gameObject.SetActive(visible);
            _answerText.gameObject.SetActive(visible);
            _giveAnswer.interactable = visible;
            _answerText.interactable = visible;
        }
        _currentQuest = quest.Name;
        _currentCriterion = quest.CriterionForGraduation;
        _criterionText.gameObject.SetActive(visible);
        _criterionText.text = _currentCriterion;
        _criterionValue.gameObject.SetActive(visible);
        _criterionValue.value = 0f;

        _phaseBox.SetActive(visible);               
    }

    public QuestEntity GetMark()
    {
        return new QuestEntity()
        {
            QuestName = _currentQuest,
            Criterion = _currentCriterion,
            Grade = Convert.ToInt32(_criterionValue.value)
        };
    }

    #region == Работа с фазами квеста ==
    private void InitPhaseButtons()
    {
        _nextPhaseButton.onClick.AddListener(SetNextPhase);
    }

    private void InitPhase(Phase phase)
    {
        _nextPhaseButton.gameObject.SetActive(true);        
        _phaseDescription.text = phase.Description;
        _phaseName.text = $"Следующая фаза: {phase.Name}";
        _phaseCount.text = $"Фазы ''{_currentQuest}'' ({_currentPhaseIndex}-{_phases.Count}):";
    }

    private void SetNextPhase()
    {
        if (_currentPhaseIndex < _phases.Count)
        {
            var phase = _phases[_currentPhaseIndex];
            phase.Complete();
            ++_currentPhaseIndex;

            // фаз больше не осталось
            if (_currentPhaseIndex >= _phases.Count)
            {
                ClearPhaseFields();                
            }
            // если все ок, ставим следующую фазу
            else
            {
                phase = _phases[_currentPhaseIndex];
                InitPhase(phase);
            }            
        }
    }

    private void ClearPhaseFields()
    {
        _nextPhaseButton.gameObject.SetActive(false);
        if (_phases != null)
        {
            _phaseCount.text = $"Фазы квеста ({_currentPhaseIndex}-{_phases.Count}):";
        }
        else
        {
            _phaseCount.text = $"Фазы квеста:";
        }

        _phaseDescription.text = "Игра завершится автоматически при соблюдении условий";
        _phaseName.text = "Фаз не осталось";
    }
    #endregion
}
