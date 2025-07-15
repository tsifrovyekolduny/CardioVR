using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Narrator : MonoBehaviour, INarrator
{
    private VisibilityAnimator _visibilityAnimator;
    private Canvas _canvas;
    private TMP_Text _hintText;
    [SerializeField]
    private float _timePerCharacter = 0.05f;
    [SerializeField]
    private float _minDisplayTime = 1.0f;

    public void Start()
    {
        _canvas = GetComponent<Canvas>();
        _visibilityAnimator = GetComponent<VisibilityAnimator>();
        _hintText = GetComponentInChildren<TMP_Text>();

        _visibilityAnimator.Hide(true);  
        _canvas.worldCamera = Camera.main;
    }

    public void Play(string phrase)
    {
        StartCoroutine(PlayCoroutine(phrase));        
    }

    private IEnumerator PlayCoroutine(string phrase)
    {
        _hintText.text = phrase;
        _visibilityAnimator.Show();
        Debug.Log(phrase);

        float timeToWait = CalculateTimeForPhrase(phrase);

        yield return new WaitForSeconds(timeToWait);

        _visibilityAnimator.Hide();
    }

    private float CalculateTimeForPhrase(string phrase)
    {        

        float calculatedTime = phrase.Length * _timePerCharacter;
        return Mathf.Max(calculatedTime, _minDisplayTime);
    }
}