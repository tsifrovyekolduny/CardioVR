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
    private List<IQuestController> _listOfQuestControllers;
    private IQuestStateController _stateController;
    private IQuestVisualController _visualController;
    private IQuestNarratorController _narratorController;
    private IQuestLogic _logic;

    [SerializeField] private int _questId = 0;

    public GameObject GameObject => gameObject;

    private void Awake()
    {
        // Инициализируем зависимости
        _stateController = GetComponent<IQuestStateController>();
        _visualController = GetComponent<IQuestVisualController>();
        _narratorController = GetComponent<IQuestNarratorController>();
        _logic = GetComponent<IQuestLogic>();

        _listOfQuestControllers = new List<IQuestController>()
        {
            _stateController, _visualController, _narratorController, _logic
        };

        _stateController.OnStarted += StartGame;
        _stateController.OnCompleted += Finish;        
    }

    public void Start()
    {
        _visualController.Hide(instant: true);
        _narratorController.PlayGreeting();
    }

    public void StartGame()
    {        
        _visualController.Show();
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
        if(_listOfQuestControllers == null)
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