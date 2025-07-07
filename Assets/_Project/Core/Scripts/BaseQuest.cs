using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public abstract class BaseQuest : MonoBehaviour, IQuest
{
    public int QuestNumber;
    [SerializeField]
    private NarratorPhraseScriptable _phraseScriptable;
    private Narrator _narrator;
    private SaveSystem _saveSystem;
    private bool _childsEnabled = false;
    private bool _isFinished = false;

    [Inject]
    private void Construct(SaveSystem saveSystem, Narrator narrator)
    {
        _saveSystem = saveSystem;
        _narrator = narrator;
    }

    void Start()
    {
        EnableChildGameObjects(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isFinished)
        {
            StartGame();
        }        
    }

    private void Update()
    {
        if (_childsEnabled)
        {
            if (IsFinished())
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

        _childsEnabled = status;
    }

    public void FinishGame()
    {
        _narrator.Play(_phraseScriptable.End[0]);
        _saveSystem.Save(QuestNumber);
        EnableChildGameObjects(false);
        _isFinished = true;
    }

    public virtual void GiveCongrats()
    {
        _narrator.Play(_phraseScriptable.Supports[0]);
    }

    public void GiveHint()
    {
        _narrator.Play(_phraseScriptable.Hints[0]);
    }

    abstract public bool IsFinished();

    public void StartGame()
    {
        _narrator.Play(_phraseScriptable.Greetings[0]);
        EnableChildGameObjects(true);
    }
}

