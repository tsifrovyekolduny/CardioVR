public interface IQuest
{    
    void StartGame();
    void FinishGame();
    bool IsFinished();

    // todo не уверен
    void GiveHint(string hint);
    void GiveCongrats();
}
