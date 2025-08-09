using UnityEngine;
using Zenject;

public class QuestAudioController : MonoBehaviour, IQuestNarratorController
{
    [SerializeField] private NarratorPhraseScriptable _phrases;

    private INarrator _narrator;

    [Inject] 
    private void Construct(INarrator narrator)
    {
        _narrator = narrator;
    }

    public void PlayGreeting() => _narrator.Play(_phrases.Greetings[0]);
    public void PlayHint(string hint) => _narrator.Play(hint);
    public void PlayCongrats() => _narrator.Play(_phrases.Congrats[0]);
    public void PlayEnd() => _narrator.Play(_phrases.End[0]);
}