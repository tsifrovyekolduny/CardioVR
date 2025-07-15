using UnityEngine;

public class QuestTile : BaseTile, IQuestTile
{
    public override TileType tileType => TileType.Quest;

    [SerializeField] private GameObject _questPlace;
    public IQuest Quest { get; private set; }
    public GameObject QuestPlace => _questPlace;

    public void LoadQuest(IQuest quest)
    {
        Quest = quest;
        // ����� ����� �������� ������ ������������� ������
    }

    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        // �������������� ������ ��� ����� �� ���� ������
    }
}