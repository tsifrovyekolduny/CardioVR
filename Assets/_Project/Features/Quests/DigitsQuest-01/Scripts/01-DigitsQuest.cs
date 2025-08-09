using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DigitsQuest : MonoBehaviour, IQuestLogic
{
    [SerializeField]
    private List<FlipableCardUI> _cards;
    [SerializeField] // для дебага
    private int _currentNumber = -1;
    private int _maxNumber = -1;

    public void Start()
    {
        _cards = GetComponentsInChildren<FlipableCardUI>().ToList();        
    }

    public void StartLogic()
    {
        InitAllCards();
    }

    public bool IsCompleted()
    {
        bool isEnd = _maxNumber == _currentNumber;
        bool isAllFlipped = _cards.Where(card => card.IsOpened).Count() == _cards.Count();
        return isEnd && isAllFlipped;
    }      
    private void TemporaryShowAllCards()
    {
        foreach (var card in _cards)
        {
            card.Flip(open: true, temporary: true);
        }
    }

    void CloseAllCards()
    {
        foreach (var card in _cards)
        {
            card.Flip(open: false, temporary: false);
        }
    }

    private void InitAllCards()
    {
        List<int> numbers = GenerateNumbers(_cards.Count);
        _maxNumber = numbers.Max();

        for (int cardIndex = 0; cardIndex < _cards.Count; ++cardIndex)
        {
            var card = _cards[cardIndex];
            int injectNumber = numbers[cardIndex];


            card.SetNumber(injectNumber);
            card.Flip(open: true, temporary: true);
            card.OnCardOpen += () => CheckNumber(injectNumber);
        }
    }

    // Todo - для детей маленькие числа. Для больших - трехзначные числа
    private List<int> GenerateNumbers(int count)
    {
        List<int> list = new List<int>();
        for (int number = 0; number < count; ++number)
        {
            list.Add(number);
        }

        list = list.OrderBy(i => UnityEngine.Random.value).ToList();

        return list;
    }

    public void CheckNumber(int number)
    {        
        // проиграл, старое число больше нового
        if (_currentNumber > number)
        {
            _currentNumber = -1;
            CloseAllCards();
        }
        // идем дальше, старое число меньше нового
        else
        {
            _currentNumber = number;
        }

    }
}
