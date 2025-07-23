using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManager : IQuestManager, IInitializable
{
    private readonly DiContainer _container;

    private QuestPrefabsObject _questPrefabsObject;
    private List<GameObject> _questPrefabs = new List<GameObject>();
    private int _currentQuestIndex = 0;
    private IQuest _currentQuest;

    [Inject]
    public QuestManager(DiContainer container, QuestPrefabsObject questPrefabsObject)
    {
        _container = container;
        _questPrefabsObject = questPrefabsObject;
    }

    public void Initialize()
    {
        PreloadQuest();
    }

    private void PreloadQuest()
    {
        foreach (var prefab in _questPrefabsObject.QuestPrefabs)
        {
            var quest = prefab.GetComponent<IQuest>();

            if (quest == null)
            {
                Debug.LogWarning("У квеста нет интерфейса!");
                continue;
            }

            _questPrefabs.Add(prefab);
        }
    }

    public IQuest GetNextQuest()
    {
        if (_currentQuestIndex >= _questPrefabs.Count)
            return null;

        var questPrefab = _questPrefabs[_currentQuestIndex];
        _currentQuest = _container.InstantiatePrefabForComponent<IQuest>(questPrefab);
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