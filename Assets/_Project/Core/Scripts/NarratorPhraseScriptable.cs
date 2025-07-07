using UnityEngine;

[CreateAssetMenu(fileName = "NarratorPhraseScriptable", menuName = "Scriptable Objects/NarratorPhrase")]
public class NarratorPhraseScriptable : ScriptableObject
{
    [SerializeField] public string[] Greetings;
    [SerializeField] public string[] Hints;
    [SerializeField] public string[] Supports;
    [SerializeField] public string[] End;
}
