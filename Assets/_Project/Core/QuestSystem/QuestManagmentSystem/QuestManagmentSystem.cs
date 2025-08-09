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
        foreach (IQuest quest in questPrefabsObject.QuestPrefabs)
        {
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
        IQuest quest = _questPrefabs.Dequeue();
        IQuestVisualController questVisualController = quest.GetQuestController<IQuestVisualController>();

        IQuest questInctance = _container.InstantiatePrefabForComponent<IQuest>(quest.GameObject);
        questInctance.GameObject.tag = "Decor";
        questVisualController.SetParent(questTile.QuestPlace.transform);
        questVisualController.SetLocalPosition(Vector3.zero);
        questVisualController.SetLocalRotation(Quaternion.identity);
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
            
            quest.GetQuestController<IQuestStateController>().OnCompleted += tile.RequestNextTile;
            Debug.Log("Квест был заспавнен");
        }
    }

    private IEnumerator DelayedStart(IQuest quest)
    {
        // TODO настраиваемое время, вызов из панели оператора
        yield return new WaitForSeconds(3f);
        quest.GetQuestController<IQuestStateController>().StartGame();
    }

    public bool AreAllQuestsCompleted() => _currentQuestIndex >= _questPrefabs.Count;
}