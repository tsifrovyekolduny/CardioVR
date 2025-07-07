using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DigitsQuest : BaseQuest
{
    [SerializeField]
    private List<FlipableCardUI> _cards;
    [SerializeField] // для дебага
    private int _currentNumber = -1;
    private int _maxNumber = -1;

    public override bool IsFinished()
    {
        return _currentNumber == _maxNumber;
    }

    public override void StartGame()
    {
        base.StartGame();
        InitAllCards();        
    }

    private void TemporaryShowAllCards()
    {
        foreach(var card in _cards)
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

        for(int cardIndex = 0; cardIndex < _cards.Count; ++cardIndex)
        {
            var card = _cards[cardIndex];
            card.SetNumber(numbers[cardIndex]);
            card.Flip(open: true, temporary: true);
        }        
    }

    // Todo - для детей маленькие числа. Для больших - трехзначные числа
    private List<int> GenerateNumbers(int count)
    {
        List<int> list = new List<int>();
        for(int number = 0;  number < count; ++number)
        {
            list.Add(number);
        }

        list = list.OrderBy(i => UnityEngine.Random.value).ToList();

        return list;
    }

    public void CheckNumber(int number)
    {
        // проиграл, старое число больше нового
        if(_currentNumber > number)
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
