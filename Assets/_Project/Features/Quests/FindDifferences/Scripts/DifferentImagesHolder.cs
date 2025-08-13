using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(Image))]
public class DifferentImagesHolder : MonoBehaviour
{
    [SerializeField] string _remainedText = "Осталось {0} отличий";
    [SerializeField] string _completedText = "Завершено!";

    private TMP_Text _diffRemainedLabel;
    private bool _isCompleted = false;
    private DifferenceSpotsPair[] _spotsPairs;
    public bool IsCompleted => _isCompleted;      
    private void Start()
    {
        _spotsPairs = GetComponentsInChildren<DifferenceSpotsPair>();
        _diffRemainedLabel = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        int remainedCount = _spotsPairs.Where(g => !g.Founded).Count();

        if(remainedCount == 0)
        {
            _diffRemainedLabel.text = _completedText;
            _isCompleted = true;
        }
        else
        {
            _diffRemainedLabel.text = string.Format(_remainedText, remainedCount);            
            _isCompleted = false;
        }
    }
}
