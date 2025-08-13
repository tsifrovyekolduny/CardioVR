using UnityEngine;
using UnityEngine.UI;

public class DifferenceSpotsPair : MonoBehaviour
{
    private bool _isFounded;
    public bool Founded => _isFounded;
    private Button[] _spots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spots = GetComponentsInChildren<Button>();
        foreach (var spot in _spots) {            
            spot.onClick.AddListener(() => OnButtonClick());
        }
    }
    
    void OnButtonClick()
    {
        foreach (var spot in _spots)
        {
            spot.interactable = false;
            spot.onClick.RemoveAllListeners();
        }
    }
}
