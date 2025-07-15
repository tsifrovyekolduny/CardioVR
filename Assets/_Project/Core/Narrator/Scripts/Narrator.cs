using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Narrator : MonoBehaviour, INarrator
{
    private VisibilityAnimator _visibilityAnimator;
    private TMP_Text _hintText;

    public void Start()
    {
        _visibilityAnimator = GetComponent<VisibilityAnimator>();
        _hintText = GetComponentInChildren<TMP_Text>();

        _visibilityAnimator.Hide(true);
    }

    public void Play(string phrase)
    {
        // Todo
        Debug.Log(phrase);
    }
}