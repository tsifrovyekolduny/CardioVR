using UnityEngine;

public class MakeOrderQuest : WhichQuest
{
    protected override void SetNextWord()
    {
        string word = _words[_currentWordIndex];
        _wordLabel.text = $" уда ты {word}?";
        ++_currentWordIndex;
    }
}
