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
            Debug.Log(prefab.name);
            if (quest == null)
            {
                Debug.LogError($"{prefab.name} �� �������� ����������� �����������");
                continue;
            }

            temporaryQuestQueue.Enqueue(quest);
        }
        Debug.Log(temporaryQuestQueue.ToArray().ToString());
        return temporaryQuestQueue;
    }

    public IQuest CreateQuest()
    {
        if (AreAllQuestsCompleted())
        {
            return null;
        }
        Debug.Log(_questPrefabs.ToArray().ToString());
        IQuest questPrefab = _questPrefabs.Dequeue();
        Debug.Log(questPrefab.QuestGameObject.name);
        IQuest questInctance = _container.InstantiatePrefabForComponent<IQuest>(questPrefab.QuestGameObject);

        return questInctance;
    }

    public void SpawnQuest(ITile tile)
    {
        Debug.Log("����������� ����� � ���������");
        if (tile is IQuestTile questTile)
        {
            IQuest quest = CreateQuest();
            Debug.Log("����� ��� �������");   
            quest.OnQuestFinished += tile.RequestNextTile;
            quest.Parent = questTile.QuestPlace.transform;
            quest.LocalPosition = Vector3.zero;
            quest.LocalRotation = Quaternion.identity;
            quest.StartGame(); //TODO: �������� ������ ������ ����
            Debug.Log("����� ��� ���������");
        }
    }

    public bool AreAllQuestsCompleted() => _currentQuestIndex >= _questPrefabs.Count;
}