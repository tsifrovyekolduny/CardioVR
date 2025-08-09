using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IQuestStateController))]
public class BaseQuest : MonoBehaviour, IQuest
{
    private IQuestStateController _stateController;
    private IQuestVisualController _visualController;
    private IQuestNarratorController _audioController;
    private IQuestLogic _logic;

    public event Action OnCompleted;

    [SerializeField] private int _questId = 0;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        // Инициализируем зависимости
        _stateController = GetComponent<IQuestStateController>();
        _visualController = GetComponent<IQuestVisualController>();
        _audioController = GetComponent<IQuestNarratorController>();
        _logic = GetComponent<IQuestLogic>();
    }

    public void Start()
    {
        _visualController.Hide(instant: true);        
    }

    public void Finish()
    {
        _stateController.Complete();
        _audioController.PlayEnd();        
        OnCompleted?.Invoke();
    }

    public bool IsCompleted() => _stateController.CurrentState == QuestStates.Finished;
}