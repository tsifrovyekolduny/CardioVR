using UnityEngine;

public class QuestStateController : MonoBehaviour, IQuestStateController
{
    public QuestStates CurrentState { get; private set; } = QuestStates.NotStarted;

    public void StartGame()
    {
        CurrentState = QuestStates.Started;
        var visualController = GetComponent<QuestVisualController>();
        visualController.Show();
    }

    public void CompleteGame()
    {
        CurrentState = QuestStates.Finished;
    }
}