using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WhichQuest : MonoBehaviour, IQuestLogic
{
    [SerializeField]
    private string[] _words = new string[5] { "настроение",
        "радость", "погода", "дорога", "страна" };
    [SerializeField] private float _timeBetweenWords;

    private int _currentWordIndex = 0;
    private TMP_Text _wordLabel;
    private TextVisibilityChanger _wordVisChanger;
    private IQuestPhasable _phasable;

    private void Start()
    {
        _wordVisChanger = GetComponentInChildren<TextVisibilityChanger>();
        _wordLabel = _wordVisChanger.GetComponentInChildren<TMP_Text>();
        _phasable = GetComponent<IQuestPhasable>();
    }

    public bool IsCompleted()
    {
        return _phasable.IsPhasesGone();
    }

    public void StartLogic()
    {
        throw new System.NotImplementedException();
    }

    public void NextWord()
    {
        if (_currentWordIndex < _words.Length)
        {
            StartCoroutine(ShowWordAfterTime());
        }
    }

    private void SetNextWord()
    {
        string word = _words[_currentWordIndex];
        _wordLabel.text = word.ToUpper();
        ++_currentWordIndex;
    }

    private IEnumerator ShowWordAfterTime()
    {
        // время между словами должно быть выше, чем анимация затухания
        _wordVisChanger.ChangeVisibility(_wordLabel.gameObject.name, false);
        yield return new WaitForSeconds(_timeBetweenWords);

        SetNextWord();

        _wordVisChanger.ChangeVisibility(_wordLabel.gameObject.name, true);
    }
}
