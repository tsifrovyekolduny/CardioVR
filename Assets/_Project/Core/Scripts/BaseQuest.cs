using UnityEngine;

public abstract class BaseQuest : MonoBehaviour, IQuest
{
    abstract public void FinishGame();


    abstract public void GiveCongrats();

    abstract public void GiveHint();

    public bool IsFinished();

    public void StartGame();
}

