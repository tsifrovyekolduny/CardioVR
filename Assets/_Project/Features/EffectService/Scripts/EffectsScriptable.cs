using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectsScriptable", menuName = "Scriptable Objects/Effects")]
public class EffectsScriptable : ScriptableObject
{
    public List<EffectConfig> ListOfEffects;
}

[Serializable]
public class EffectConfig
{
    public VisualEffectType type;
    public Effect prefab;
}