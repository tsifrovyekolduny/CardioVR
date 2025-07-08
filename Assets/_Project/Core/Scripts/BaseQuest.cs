using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public abstract class BaseQuest : MonoBehaviour, IQuest
{
    public int QuestNumber = 0;
    [SerializeField]
    private NarratorPhraseScriptable _phraseScriptable;
    private Narrator _narrator;
    private SaveSystem _saveSystem;

    // для просмотра состояния квеста в инспекторе
    [SerializeField]
    private QuestStates _currentState = QuestStates.NotStarted;    

    [Inject]
    private void Construct(SaveSystem saveSystem, Narrator narrator)
    {
        _saveSystem = saveSystem;
        _narrator = narrator;
    }

    public virtual void Start()
    {
        EnableChildGameObjects(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Нельзя начать игру, если она в процессе или завершена
        if (_currentState == QuestStates.NotStarted)
        {
            StartGame();
        }        
    }

    private void Update()
    {
        // Проверка условия только, когда игра началась
        if (_currentState == QuestStates.Started)
        {
            if (IsFinished() && _currentState != QuestStates.Finished)
            {
                FinishGame();
            }
        }
    }

    protected void EnableChildGameObjects(bool status)
    {
        foreach(Transform child in transform)
        {
            if(child.tag != "Decor")
            {
                child.gameObject.SetActive(status);
            }            
        }        
    }

    public void FinishGame()
    {
        _currentState = QuestStates.Finished;
        _narrator.Play(_phraseScriptable.End[0]);        
        EnableChildGameObjects(false);
        _saveSystem.Save(QuestNumber, _currentState);
    }

    public virtual void GiveCongrats()
    {
        _narrator.Play(_phraseScriptable.Congrats[0]);
    }

    public void GiveHint()
    {
        _narrator.Play(_phraseScriptable.Hints[0]);
    }

    abstract public bool IsFinished();

    public virtual void StartGame()
    {
        _narrator.Play(_phraseScriptable.Greetings[0]);
        EnableChildGameObjects(true);
        _currentState = QuestStates.Started;
    }
}

