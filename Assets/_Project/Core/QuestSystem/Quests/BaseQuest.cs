using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO не хватае Dispose

[RequireComponent(typeof(IQuestStateController))]
[RequireComponent(typeof(IQuestVisualController))]
[RequireComponent(typeof(IQuestNarratorController))]
public class BaseQuest : MonoBehaviour, IQuest
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
    }

    public void Finish()
    {
        _stateController.CompleteGame();
        _visualController.Hide();
        _narratorController.PlayEnd();
    }

    public bool IsCompleted() => _stateController.CurrentState == QuestStates.Finished;

    T IQuest.GetQuestController<T>()
    {
        return _listOfQuestControllers.OfType<T>().First();
    }
}