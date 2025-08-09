using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestManagmentSystem : IQuestManagmentSystem 
{
    private int _currentQuestIndex = 0;

    private readonly DiContainer _container;
    private readonly Queue<IQuest> _questPrefabs;

    [Inject]
    public QuestManagmentSystem(DiContainer container, QuestPrefabsObject questPrefabsObject)
    {
        _container = container;
        _questPrefabs = PreloadQuest(questPrefabsObject);
    }

    private Queue<IQuest> PreloadQuest(QuestPrefabsObject questPrefabsObject)
    {
        var temporaryQuestQueue = new Queue<IQuest>();
        foreach (GameObject prefab in questPrefabsObject.QuestPrefabs)
        {

            IQuest quest = prefab.GetComponent<IQuest>();
            if (quest == null)
            {
                Debug.LogError($"{prefab.name} не обладает необходимым интерфейсом");
                continue;
            }

            temporaryQuestQueue.Enqueue(quest);
        }
        return temporaryQuestQueue;
    }

    public IQuest CreateQuest(IQuestTile questTile)
    {
        if (AreAllQuestsCompleted())
        {
            return null;
        }
        IQuest questPrefab = _questPrefabs.Dequeue();
        IQuest questInctance = _container.InstantiatePrefabForComponent<IQuest>(questPrefab.QuestGameObject);
        questInctance.QuestGameObject.tag = "Decor";
        questInctance.Parent = questTile.QuestPlace.transform;
        questInctance.LocalPosition = Vector3.zero;
        questInctance.LocalRotation = Quaternion.identity;
        return questInctance;
    }

    public void SpawnQuest(ITile tile)
    {
        Debug.Log("Запрашиваем квест у менеджера");
        if (tile is IQuestTile questTile)
        {
            IQuest quest = CreateQuest(questTile);
            Debug.Log("Квест был получен");
            if (quest is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.StartCoroutine(DelayedStart(quest));
            }
            
            quest.OnQuestFinished += tile.RequestNextTile;
            Debug.Log("Квест был заспавнен");
        }
    }

    private IEnumerator DelayedStart(IQuest quest)
    {
        yield return null;
        quest.StartGame();
    }

    public bool AreAllQuestsCompleted() => _currentQuestIndex >= _questPrefabs.Count;
}