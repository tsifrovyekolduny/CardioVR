using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(BoxCollider))]

// todo фразы по умолчанию говорят первый элемент из списка
public abstract class BaseQuest : MonoBehaviour, IQuest
{
    public event Action OnQuestFinished;
    public int QuestNumber = 0;
    [SerializeField]
    private NarratorPhraseScriptable _phraseScriptable;
    private INarrator _narrator;
    private IOperator _operator;
    private SaveSystem _saveSystem;
    public GameObject QuestGameObject => gameObject;
    public Quaternion LocalRotation { 
        get => transform.localRotation; 
        set => transform.localRotation = value; 
    }
    public Vector3 LocalPosition
    {
        get => transform.localPosition;
        set => transform.localPosition = value;
    }

    public Transform Parent
    {
        get => transform.parent;
        set => transform.SetParent(value);
    }

    // для просмотра состояния квеста в инспекторе
    [SerializeField]
    private QuestStates _currentState = QuestStates.NotStarted;

    public QuestStates CurrentState => _currentState;
    [Inject]
    private void Construct(SaveSystem saveSystem, INarrator narrator, IOperator @operator)
    {
        _operator = @operator;
        _saveSystem = saveSystem;
        _narrator = narrator;
    }

    public virtual void Start()
    {
        EnableChildGameObjects(false, true);
    }


    private void Update()
    {
        // Проверка условия только, когда игра началась
        if (_currentState == QuestStates.Started)
        {
            if (IsFinished() && _currentState != QuestStates.Finished)
            {
                FinishGame();
            }
        }
    }

    protected virtual void EnableChildGameObjects(bool status, bool instant = false)
    {
        foreach (Transform child in transform)
        {
            if (child.tag != "Decor")
            {
                // сама анимация
                var animators = child.GetComponentsInChildren<VisibilityAnimator>();
                if (animators.Length > 0)
                {
                    foreach (var animator in animators)
                    {
                        if (status)
                        {
                            animator.Show(instant);
                        }
                        else
                        {
                            animator.Hide(instant);
                        }
                    }
                }
            }
        }
        string log = status ? "showed" : "hided";
        Debug.Log($"Childs are {log}");
    }

    public void FinishGame()
    {
        OnQuestFinished?.Invoke();
        _operator.OnSessionEnd -= FinishGame;
        _operator.OnGivingHint -= GiveHint;

        _currentState = QuestStates.Finished;
        _narrator.Play(_phraseScriptable.End[0]);        
        EnableChildGameObjects(false);
        _saveSystem.Save(QuestNumber, _currentState);
    }

    public virtual void GiveCongrats()
    {
        _narrator.Play(_phraseScriptable.Congrats[0]);
    }

    public void GiveHint(string hint)
    {
        _narrator.Play(hint);
    }

    abstract public bool IsFinished();

    public virtual void StartGame()
    {
        // Подписка на события от оператора
        _operator.OnSessionEnd += FinishGame;
        _operator.OnGivingHint += GiveHint;
        _operator.QuestStarted(_phraseScriptable.Hints);

        _narrator.Play(_phraseScriptable.Greetings[0]);
        EnableChildGameObjects(true);
        _currentState = QuestStates.Started;
    }
}

