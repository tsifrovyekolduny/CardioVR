using System;
using UnityEngine;

public interface IQuest
{    
    void StartGame();
    void FinishGame();
    bool IsFinished();

    GameObject QuestGameObject { get; }
    public event Action OnQuestFinished;
    public Vector3 LocalPosition { get; set; }
    public Quaternion LocalRotation { get; set; }
    Transform Parent { get; set; }
    // todo не уверен
    void GiveHint(string hint);
    void GiveCongrats();
}
