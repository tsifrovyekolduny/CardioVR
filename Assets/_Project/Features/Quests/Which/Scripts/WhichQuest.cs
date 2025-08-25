using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhichQuest : MonoBehaviour, IQuestLogic
{
    [SerializeField]
    protected string[] _words = new string[5] { "настроение",
        "радость", "погода", "дорога", "страна" };
    [SerializeField] private float _timeBetweenWords;

    protected int _currentWordIndex = 0;
    protected TMP_Text _wordLabel;
    protected Image _wordBG;
    protected ConcreteVisibilityChanger _wordVisChanger;
    protected IQuestPhasable _phasable;

    private void Start()
    {
        _wordVisChanger = GetComponentInChildren<ConcreteVisibilityChanger>();
        _wordLabel = _wordVisChanger.GetComponentInChildren<TMP_Text>();
        _wordBG = _wordVisChanger.GetComponentInChildren<Image>();
        _phasable = GetComponent<IQuestPhasable>();
    }

    public bool IsCompleted()
    {
        return _phasable.IsPhasesGone();
    }

    public void StartLogic()
    {        
    }

    public void NextWord()
    {
        if (_currentWordIndex < _words.Length)
        {
            StartCoroutine(ShowWordAfterTime());
        }
    }

    protected virtual void SetNextWord()
    {
        string word = _words[_currentWordIndex];
        _wordLabel.text = word[0].ToString().ToUpper() + word.Substring(1);
        ++_currentWordIndex;
    }

    private IEnumerator ShowWordAfterTime()
    {
        // время между словами должно быть выше, чем анимация затухания
        SetActiveWord(false);
        yield return new WaitForSeconds(_timeBetweenWords);

        SetNextWord();
        SetActiveWord(true);        
    }

    private void SetActiveWord(bool active, bool instant = false)
    {
        if(!active && instant)
        {
            _wordLabel.gameObject.SetActive(active);
            _wordBG.gameObject.SetActive(active);
        }
        _wordLabel.gameObject.SetActive(true);
        _wordBG.gameObject.SetActive(true);

        _wordVisChanger.ChangeVisibility(_wordLabel.gameObject.name, active);
        _wordVisChanger.ChangeVisibility(_wordBG.gameObject.name, active);
    }
}
