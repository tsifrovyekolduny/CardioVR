using FindColorExtension;
using ModestTree;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindColorQuest : MonoBehaviour, IQuestLogic
{
    [SerializeField]
    private List<IColorButton> _colorButtons = new List<IColorButton>();
    [SerializeField]
    private IColorButton _currentColorButton;
    [SerializeField]
    private int _currentColorButtonIndex = 0;

    struct TextTargetColors
    {
        public Color TextColor;
        public Color TargetColor;
        public TextTargetColors(Color textColor, Color targetColor)
        {
            this.TextColor = textColor;
            this.TargetColor = targetColor;
        }
    }

    // повтор порядка из примера
    List<TextTargetColors> _hardcodedColors = new List<TextTargetColors>()
    {
        new(Color.yellow, Color.yellow),
        new(NewColor.orange, Color.blue),
        new(Color.blue, NewColor.orange),
        new(Color.red, Color.red),
        new(Color.black, Color.cyan),
        new(Color.cyan, NewColor.violet),
        new(Color.black, Color.white),
        new(Color.green, Color.black),
        new(Color.green, Color.red),
        new(NewColor.orange, NewColor.orange),
        new(Color.red, Color.blue),
        new(NewColor.violet, Color.yellow),
        new(Color.blue, Color.green),
        new(Color.green, NewColor.purple),
        new(Color.cyan, Color.red),
        new(Color.yellow, Color.green),
        new(NewColor.orange, Color.black),
        new(Color.black, Color.blue),
        new(Color.blue, Color.white),
        new(NewColor.purple, NewColor.violet)
    };


    public bool IsCompleted()
    {
        if (_colorButtons.Count > 0)
        {
            return _colorButtons.All(c => c.IsClosed.Equals(true));
        }
        return false;
    }

    public void StartLogic()
    {        
        _currentColorButton = _colorButtons[_currentColorButtonIndex];
        _currentColorButton.Select();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        if (_colorButtons.Count == 0)
        {
            _colorButtons = GetComponentsInChildren<IColorButton>().ToList();
        }

        for (int indButton = 0; indButton < _colorButtons.Count; ++indButton)
        {
            var button = _colorButtons[indButton];
            button.PressButton += CompareWithCurrentColor;
            button.Init(_hardcodedColors[indButton].TextColor, _hardcodedColors[indButton].TargetColor);
        }

        Debug.Log($"Count of CB: {_colorButtons.Count}");        
    }

    private void CompareWithCurrentColor(Color color)
    {
        if (_currentColorButton.TargetColor == color)
        {
            SetNextColor();
        }
    }

    private void SetNextColor()
    {
        if (_currentColorButtonIndex < _colorButtons.Count)
        {
            _currentColorButton.Hide();

            _currentColorButtonIndex++;
            _currentColorButton = _colorButtons[_currentColorButtonIndex];
            _currentColorButton.Select();
        }
    }
}
