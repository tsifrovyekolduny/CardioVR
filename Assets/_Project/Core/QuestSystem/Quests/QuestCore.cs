using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// TODO можно так делать, пока не появятся иные QuestVisualController
[RequireComponent(typeof(QuestStateController))]
[RequireComponent(typeof(QuestVisualController))]
[RequireComponent(typeof(QuestNarratorController))]
[RequireComponent(typeof(QuestPhaseController))]

// TODO не хватае Dispose
public class QuestCore : MonoBehaviour, IQuest
{
    [SerializeField] private string _name = "[Не обозначено]";
    [SerializeField] private string _criterionForGraduation = "[Не обозначено]";
    [SerializeField] private bool _isDesignedForPlayerAnswers;

    private List<IQuestController> _listOfQuestControllers;
    private IQuestStateController _stateController;
    private IQuestVisualController _visualController;
    private IQuestNarratorController _narratorController;
    private IQuestLogic _logic;    
    private IQuestController _phasable;    

    public GameObject GameObject => gameObject;

    public string Name => _name;
    public string CriterionForGraduation => _criterionForGraduation;
    public bool IsDesignedForPlayerAnswers => _isDesignedForPlayerAnswers;

    private void Awake()
    {
        // Инициализируем зависимости
        _stateController = GetComponent<IQuestStateController>();
        _visualController = GetComponent<IQuestVisualController>();
        _narratorController = GetComponent<IQuestNarratorController>();
        _logic = GetComponent<IQuestLogic>();
        _phasable = GetComponent<IQuestPhasable>();

        _listOfQuestControllers = new List<IQuestController>()
        {
            _stateController, _visualController, _narratorController, _logic, _phasable
        };

        _stateController.OnStarted += StartGame;
        _stateController.OnCompleted += Finish;
    }

    public void Start()
    {
        _visualController.Hide(instant: true, firstTime: true);
        _narratorController.PlayGreeting();
    }

    public void StartGame()
    {
        _visualController.Show(instant: false, firstTime: true);
        _logic.StartLogic();
    }

    public void Finish()
    {
        _visualController.Hide();
        _narratorController.PlayEnd();
    }

    private void Update()
    {
        if (_logic.IsCompleted() && _stateController.CurrentState != QuestStates.Finished)
        {
            // TODO вызов завершения из панели оператора
            Debug.Log("Task completed");
            _stateController.CompleteGame();
        }
    }

    T IQuest.GetQuestController<T>()
    {
        if (_listOfQuestControllers == null)
        {
            return GetComponent<T>();
        }

        // Такая реализция выдачи более экономная
        else
        {
            return _listOfQuestControllers.OfType<T>().First();
        }
    }
}