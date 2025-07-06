using UnityEngine;

public abstract class BaseQuest : MonoBehaviour, IQuest
{
    [SerializeField]
    private NarratorPhraseScriptable _phraseScriptable;
    private Narrator _narrator;
    private bool _childsEnabled = false;

    private void Start()
    {
        
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

    protected void EnableChildGameObjects()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    protected void DisableChildGameObjects()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void FinishGame()
    {
        _narrator.Play(_phraseScriptable.Greetings[0]);
        _saveSystem.Save();
        DisableChildGameObjects();
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
        EnableChildGameObjects();
    }
}

