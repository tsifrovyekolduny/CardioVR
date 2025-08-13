using System;
using UnityEngine;
using UnityEngine.UI;

public class DifferenceSpotsPair : MonoBehaviour
{
    [SerializeField] Color _whenFoundedColor = Color.red;
    private bool _isFounded;
    public bool Founded => _isFounded;
    private Button[] _spots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spots = GetComponentsInChildren<Button>();
        foreach (Button spot in _spots) {            
            InitSpot(spot);            
        }
    }

    private void InitSpot(Button spot)
    {
        spot.onClick.AddListener(() => OnButtonClick());
        var colors = spot.colors;
        colors.normalColor = Color.clear;
        colors.highlightedColor = Color.clear;
        colors.selectedColor = Color.clear;        
        colors.pressedColor = Color.clear;
        colors.selectedColor = Color.clear;
        colors.disabledColor = _whenFoundedColor;
        spot.colors = colors;      
    }

    void OnButtonClick()
    {
        foreach (Button spot in _spots)
        {
            MakeFounded(spot);            
        }
    }

    void MakeFounded(Button spot)
    {
        _isFounded = true;
        spot.interactable = false;
        spot.onClick.RemoveAllListeners();
    }
}
