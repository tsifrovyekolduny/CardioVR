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

        IQuest questInstance = _container.InstantiatePrefabForComponent<IQuest>(quest.GameObject);
        IQuestVisualController visualsFromInstance = questInstance.GetQuestController<IQuestVisualController>();

        questInstance.GameObject.tag = "Decor";
        visualsFromInstance.SetParent(questTile.QuestPlace.transform);
        visualsFromInstance.SetLocalPosition(Vector3.zero);
        visualsFromInstance.SetLocalRotation(Quaternion.identity);
        return questInstance;
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
        yield return new WaitForSeconds(2.5f);
        quest.GetQuestController<IQuestStateController>().StartGame();
    }

    public bool AreAllQuestsCompleted() => _currentQuestIndex >= _questPrefabs.Count;
}