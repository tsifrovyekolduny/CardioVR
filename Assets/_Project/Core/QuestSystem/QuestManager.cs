using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManager : IQuestManager, IInitializable
{
    private readonly DiContainer _container;
    private QuestSettings _settings;
    private List<BaseQuest> _questPrefabs;
    private int _currentQuestIndex = 0;
    private IQuest _currentQuest;

    [Inject]
    public QuestManager(DiContainer container, QuestSettings settings)
    {
        _container = container;
        _settings = settings;
    }

    public void Initialize()
    {
        _questPrefabs = new List<BaseQuest>(_settings.QuestPrefabs);
    }

    public IQuest GetNextQuest()
    {
        if (_currentQuestIndex >= _questPrefabs.Count)
            return null;

        var questPrefab = _questPrefabs[_currentQuestIndex];
        _currentQuest = _container.InstantiatePrefabForComponent<BaseQuest>(questPrefab);
        _currentQuestIndex++;

        return _currentQuest;
    }

    public void ResetQuests()
    {
        _currentQuestIndex = 0;
        _currentQuest = null;
    }

    public IQuest GetCurrentQuest() => _currentQuest;

    public bool AreAllQuestsCompleted() => _currentQuestIndex >= _questPrefabs.Count;
}